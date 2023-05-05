import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  constructor(
    private auth: OidcSecurityService) {
  }

  ngOnInit(): void {
    this.auth.logoff().subscribe(() => {
      this.clearCookies();
    });
  }

  private clearCookies(): void {
    console.log(document.cookie);
  }
}
