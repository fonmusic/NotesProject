import { Component } from '@angular/core';
import { Admin } from 'src/app/models/admin';
import { ServiceResponse } from 'src/app/models/serviceResponse';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  user = new User();
  admin = new Admin();
  loggedIn = false;
  isAdmin = false;
  isUserMode = false;
  isAdminMode = false;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private authService: AuthService) { }

  enterAsUser() {
    this.isUserMode = true;
    this.isAdminMode = false;
    this.clearMessages();
  }

  enterAsAdmin() {
    this.isUserMode = false;
    this.isAdminMode = true;
    this.clearMessages();
  }

  registerUser(user: User) {
    this.authService.registerUser(user).subscribe(
      (response: ServiceResponse<number>) => {
        if (response.success) {
          this.successMessage = 'User registered successfully.';
        } else {
          this.errorMessage = response.message;
        }
      },
      (error) => {
        this.errorMessage = 'An error occurred during user registration.';
      }
    );
  }

  registerAdmin(admin: Admin) {
    this.authService.registerAdmin(admin).subscribe(
      (response: ServiceResponse<number>) => {
        if (response.success) {
          this.successMessage = 'Admin registered successfully.';
        } else {
          this.errorMessage = response.message;
        }
      },
      (error) => {
        this.errorMessage = 'An error occurred during admin registration.';
      }
    );
  }

  loginUser(user: User) {
    this.authService.loginUser(user).subscribe(
      (response: ServiceResponse<string>) => {
        if (response.success) {
          localStorage.setItem('authToken', response.data);
          this.loggedIn = true;
          this.isAdmin = false;
          this.isUserMode = false;
          this.isAdminMode = false;
          this.clearMessages();
        } else {
          this.errorMessage = response.message;
        }
      },
      (error) => {
        this.errorMessage = 'An error occurred during user login.';
      }
    );
  }

  loginAdmin(admin: Admin) {
    this.authService.loginAdmin(admin).subscribe(
      (response: ServiceResponse<string>) => {
        if (response.success) {
          localStorage.setItem('authToken', response.data);
          this.loggedIn = true;
          this.isAdmin = true;
          this.isUserMode = false;
          this.isAdminMode = true;
          this.clearMessages();
        } else {
          this.errorMessage = response.message;
        }
      },
      (error) => {
        this.errorMessage = 'An error occurred during admin login.';
      }
    );
  }

  logout() {
    localStorage.removeItem('authToken');
    this.loggedIn = false;
    this.isAdmin = false;
    this.isUserMode = false;
    this.isAdminMode = false;
    this.clearMessages();
  }

  clearMessages() {
    this.errorMessage = '';
    this.successMessage = '';
  }
}
