import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { RxState } from "@rx-angular/state";
import { catchError, Observable, of, Subject, tap } from "rxjs";
import { AuthenticateService } from "src/app/services/authenticate.service";
import { UserService } from "src/app/services/user.service";
import { LoginState } from "../states";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
  providers: [RxState],
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  onSubmit = new Subject<void>();
  get state$(): Observable<LoginState> {
    return this._state.select();
  }

  constructor(
    private _userService: UserService,
    private _authService: AuthenticateService,
    private _fb: FormBuilder,
    private _router: Router,
    private _state: RxState<LoginState>,
  ) {
    this._state.set({
      hasError: false,
    });
  }

  ngOnInit(): void {
    this.initForm();
    this.manageEvents();
    this.connectState();
  }

  initForm(): void {
    this.form = this._fb.group({
      username: ["", [Validators.required]],
      password: ["", [Validators.required]],
    });
  }

  private connectState(): void {
    this._state.connect(
      this.form.valueChanges,
      (prev, curr: { username: string; password: string }) => {
        return {
          ...prev,
          username: curr.username,
          password: curr.password,
        };
      },
    );
  }

  private manageEvents() {
    this._state.hold(this.onSubmit, () => {
      const valid = this.form.valid;
      this._state.set({
        hasError: !valid,
      });

      if (!valid) {
        return;
      }

      this._userService
        .login(this._state.get("username"), this._state.get("password"))
        .pipe(
          catchError((err: { statusCode: string }) => {
            return of({ token: "", statusCode: err.statusCode });
          }),
        )
        .subscribe({
          next: response => {
            this._state.set({
              statusCode: response.statusCode,
              hasError: !response.token,
            });

            if (response.token) {
              this._authService.persistToken(response.token);
              this._router.navigate(["/"]);
            }
          },
        });
    });
  }
}
