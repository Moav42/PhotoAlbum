import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService, UserService } from '../Shared/Services';
import { User } from '../Shared/Models/UserAccount';
import { AlertService } from '../alert';


@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
   loginForm: FormGroup;
    currentUser: User;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
 

    constructor(private formBuilder: FormBuilder,
         private authenticationService: AuthenticationService, 
         private userService: UserService,
         private route: ActivatedRoute, 
         private router: Router,
         private alertService: AlertService )  { 
            this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
 
        }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            oldPassword: ['', Validators.required],
            newPassword: ['', Validators.required]
        });
    }

    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.changePassword(this.currentUser.username, this.f.oldPassword.value, this.f.newPassword.value ) .pipe(first())
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            error => {
                this.error = error;
                this.loading = false;
            }
        );
        this.alertService.success('Password changed');
    }
}