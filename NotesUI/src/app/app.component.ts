import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NotesUI';

  constructor() { }

  showAuth: boolean = false;

  toggleAuth(): void {
    this.showAuth = !this.showAuth;
  }
  
}
