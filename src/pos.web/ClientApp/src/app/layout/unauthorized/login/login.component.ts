import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { RxState } from "@rx-angular/state";
import { catchError, filter, mergeMap, Observable, of, Subject } from "rxjs";
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
  onSubmitHandler$ = new Subject<{ username: string; password: string }>();
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
    const handler$ = this.onSubmitHandler$.pipe(
      mergeMap(data =>
        this._userService.login(data.username, data.password).pipe(
          catchError((err: { statusCode: string }) =>
            of({
              statusCode: err.statusCode,
              token: "",
            }),
          ),
        ),
      ),
    );

    this._state.connect(handler$, (prev, curr) => ({
      ...prev,
      statusCode: curr.statusCode,
      hasError: !curr.token,
      token: curr.token,
    }));

    this._state
      .select("token")
      .pipe(filter(x => !!x))
      .subscribe({
        next: token => {
          this._router.navigate(["/"]);
          this._authService.persistToken(token);
        },
      });
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

      this.onSubmitHandler$.next({
        username: this.form.value.username,
        password: this.form.value.password,
      });
    });
  }
}
