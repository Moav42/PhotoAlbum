import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../Shared/Services';
import { AlertService } from '../Shared/Services/alert.service';
import { UserManagerService } from '../Shared/Services/user-manager.service';

@Component({
  selector: 'app-account-creation-form',
  templateUrl: './account-creation-form.component.html',
  styleUrls: ['./account-creation-form.component.css']
})
export class AccountCreationFormComponent implements OnInit {

  registerForm: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private userService: UserManagerService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            email: ['', Validators.required, Validators.email],
            password: ['', [Validators.required, Validators.minLength(6)]],
            role: ['', Validators.required],
        });
    }


    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.registerForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.createAcc(this.registerForm.value.email, this.registerForm.value.password, this.registerForm.value.role)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', true);
                    this.router.navigate(['/admin/users']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
  }