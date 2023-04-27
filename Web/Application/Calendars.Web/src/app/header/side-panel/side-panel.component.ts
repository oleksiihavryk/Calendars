import { Component, OnInit } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.css']
})
export class SidePanelComponent implements OnInit {
  public isLoggedIn: boolean = false;

  constructor(
    public sidePanel: NavigationPanelService,
    public oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth()
      .subscribe({
        next: (value) => {
          this.isLoggedIn = value.isAuthenticated;
        }
      })
  }

  public login() : boolean {
    this.oidcSecurityService.authorize();

    this.sidePanel.close();
    return false;
  }
  public logout(): boolean {
    this.oidcSecurityService.logoff().subscribe(console.log);

    this.sidePanel.close();
    return false;
  }
}
