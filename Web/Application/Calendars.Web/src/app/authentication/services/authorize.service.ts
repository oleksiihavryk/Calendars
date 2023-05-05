import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  public isLogged: boolean = false;

  constructor(
    oidcSecurityService: OidcSecurityService,
    private router: Router) { 
    oidcSecurityService.checkAuth().subscribe((loginResponse) => {
      this.isLogged = loginResponse.isAuthenticated;
      if (this.isLogged === false && loginResponse.errorMessage !== undefined)
            console.error(loginResponse.errorMessage);
    })
  }

  public login(): Promise<boolean> {
    return this.router.navigateByUrl('/login');
  }
  public logout(): Promise<boolean> {
    return this.router.navigateByUrl('/login');
  }
  public requestLogoutIfUserIsLoggedIn(): void {
    if (this.isLogged) {
      this.logout();
    }
  }
  public requestAuthenticationIfUserIsNotLoggedIn(): void {
    if (this.isLogged === false) {
      this.login();
    }
  }
}
