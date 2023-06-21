import { Component } from '@angular/core';
import { User } from './models/user';
import { Admin } from './models/admin';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NotesUI';

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
