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
  buttonLabel: string = 'Open';

  toggleOpenClose(): void {
    this.showAuth = !this.showAuth;
    this.buttonLabel = this.showAuth ? 'Close' : 'Open';
  }
  
}
