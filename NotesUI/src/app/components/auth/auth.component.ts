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

  constructor(private authService: AuthService) { }

  registerUser(user: User) {
    this.authService.registerUser(user).subscribe();
    console.log("user is registered");
  }

  registerAdmin(admin: Admin) {
    this.authService.registerAdmin(admin).subscribe();
    console.log("admin is registered");
  }

  loginUser(user: User) {
    this.authService.loginUser(user).subscribe((response: ServiceResponse<string>) => {
      localStorage.setItem('authToken', response.data);
      this.loggedIn = true;
      this.isAdmin = false;
      console.log("user is logged in");
    });
  }

  loginAdmin(admin: Admin) {
    this.authService.loginAdmin(admin).subscribe((response: ServiceResponse<string> ) => {
      localStorage.setItem('authToken', response.data);
      this.loggedIn = true;
      this.isAdmin = true;
      console.log("admin is logged in");
      console.log(response);
    });
  }

  logout() {
    localStorage.removeItem('authToken');
    this.loggedIn = false;
    this.isAdmin = false;
    console.log("logout");
  }
}