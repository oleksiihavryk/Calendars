import { Component, OnDestroy, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  private sub: Subscription | undefined;
  private waitingTimeMs: number = 7500;
  
  public showErrorContent: boolean = false;

  constructor(
    private auth: OidcSecurityService) {
    this.auth.authorize();
  }

  ngOnInit(): void {
    this.sub = timer(this.waitingTimeMs).subscribe(n => {
      this.showErrorContent = true;
    });
  }
  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
