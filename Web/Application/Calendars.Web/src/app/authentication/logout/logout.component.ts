import { Component, OnInit, OnDestroy } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { CookieService } from 'ngx-cookie-service';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit, OnDestroy {
  private sub: Subscription | undefined;
  private waitingTimeMs: number = 7500;

  public showErrorContent: boolean = false;

  constructor(
    private auth: OidcSecurityService,
    private cookieService: CookieService) {
  }

  ngOnInit(): void {
    this.auth.logoff().subscribe(() => {
      this.clearCookies();
      
      this.sub = timer(this.waitingTimeMs).subscribe(n => {
        this.showErrorContent = true;
      })
    });
  }
  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  private clearCookies(): void {
    this.cookieService.deleteAll('/', window.location.hostname);
  }
}
