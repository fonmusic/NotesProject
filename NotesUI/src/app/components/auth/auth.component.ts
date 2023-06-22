import { Component } from '@angular/core';
import { Admin } from 'src/app/models/admin';
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

  constructor(private authService: AuthService) { }

  registerUser(user: User) {
    this.authService.registerUser(user).subscribe();
  }

  registerAdmin(admin: Admin) {
    this.authService.registerAdmin(admin).subscribe();
  }

  loginUser(user: User) {
    this.authService.loginUser(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });
  }

  loginAdmin(admin: Admin) {
    this.authService.loginAdmin(admin).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });

  }
}