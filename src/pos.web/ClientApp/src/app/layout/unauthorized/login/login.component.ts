import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthenticateService } from "src/app/services/authenticate.service";
import { UserService } from "src/app/services/user.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private _userService: UserService,
    private _authService: AuthenticateService,
    private _fb: FormBuilder,
    private _router: Router,
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.form = this._fb.group({
      username: ["", [Validators.required]],
      password: ["", [Validators.required]],
    });
  }

  submit(): void {
    if (!this.form.valid) {
      return; // todo: show msg
    }

    const username = this.form.get("username")?.value;
    const pw = this.form.get("password")?.value;

    // todo: unsubscribe
    this._userService.login(username, pw).subscribe({
      next: token => {
        if (token?.length) {
          this._authService.persistToken(token);
          this._router.navigate(["/"]);

          // todo: use redirectUrl
        }
      },
    });
  }
}
