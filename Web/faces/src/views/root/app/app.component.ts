import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { ToastModule } from 'primeng/toast'
import { SessionService } from '../../../shared/services/session.service';
import { LoginService } from '../../../shared/services/login.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBarComponent, ToastModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'faces';

  constructor(private login: LoginService){
    login.initApp();
  }
}
