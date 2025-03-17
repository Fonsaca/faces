import { Component, OnDestroy, OnInit } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCaretLeft, faTimesCircle, faUser } from '@fortawesome/free-solid-svg-icons';
import { Employee } from '../../../shared/models/employee';
import { Subject, takeUntil } from 'rxjs';
import { SessionService } from '../../../shared/services/session.service';

@Component({
  selector: 'nav-bar',
  imports: [FontAwesomeModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent implements OnInit, OnDestroy{

  iconUser = faUser
  iconSignOut = faTimesCircle

  constructor(private session: SessionService ){}

  employee?: Employee = undefined;

  private lifecycle = new Subject<void>();
  ngOnInit(): void {
    this.session.employee.pipe(takeUntil(this.lifecycle))
      .subscribe({
        next: (e) => {
          this.employee = e;
        }
      })
  }

  ngOnDestroy(): void {
    this.lifecycle.next();
  }

  signOut(): void{
    this.session.logout();
  }

}
