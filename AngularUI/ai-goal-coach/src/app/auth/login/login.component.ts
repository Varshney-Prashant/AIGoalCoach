import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserLoginRequest } from './models/user-login-request';
import { UserRegisterRequest } from './models/user-register-request';
import { AuthService } from './services/auth.service';
import * as bootstrap from 'bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email = '';
  password = '';
  regName = '';
  regEmail = '';
  regPassword = '';

  constructor(
    private auth: AuthService, 
    private router: Router,
    private toastrService: ToastrService
    ) {}

  login() {
    var loginRequest = new UserLoginRequest({emailAddress: this.email, password: this.password});
    this.auth.login(loginRequest).subscribe({
      next: (result) => {
        if(!result.isSuccess){
          this.toastrService.error("Invalid Email or Password", "Login Failed");
          return;
        }
        this.toastrService.success('Logged In Successfully', "Success!");
        localStorage.setItem('token', result.accessToken)
        this.router.navigate(['/my-goals'])
      },
      error: () => this.toastrService.error("Invalid Email or Password")
    });
  }

  register() {
    var userRegisterRequest = new UserRegisterRequest({
      emailAddress : this.regEmail,
      userName : this.regName,
      password: this.regPassword
    })

    this.auth.register(userRegisterRequest).subscribe({
      next: (result) => {
        if (result){
          const modalEl = document.getElementById('registerModal')!;
          const modal = bootstrap.Modal.getInstance(modalEl)!;
          modal.hide();
          this.regName = '';
          this.regEmail = '';
          this.regPassword = '';
          this.toastrService.success("User created successfully", "Success!");
        }
        
      },
      error: () => this.toastrService.error("Error while creating user", "User Registration failed")
    });
  }

  openRegisterModal(){
    this.regName = '';
    this.regEmail = '';
    this.regPassword = '';
    const modalEl = document.getElementById('registerModal')!;
    const modal = new bootstrap.Modal(modalEl, {
      backdrop: 'static',
      keyboard: false
    });
    modal.show();
  }
}
