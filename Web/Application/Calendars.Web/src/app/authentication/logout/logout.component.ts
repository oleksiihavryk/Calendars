import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  constructor(
    private auth: OidcSecurityService,
    private cookieService: CookieService) {
  }

  ngOnInit(): void {
    this.auth.logoff().subscribe(() => {
      this.clearCookies();
    });
  }

  private clearCookies(): void {
    this.cookieService.deleteAll();
  }
}
