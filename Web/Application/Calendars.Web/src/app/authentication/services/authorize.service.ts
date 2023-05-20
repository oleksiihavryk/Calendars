import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import jwtDecode from 'jwt-decode';

export interface IUserData {
  id: string | undefined; 
  email: string | undefined; 
  username: string | undefined; 
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {
  public isLogged: boolean = false;
  public userData: IUserData = {
    id: undefined,
    username: undefined,
    email: undefined,
  }

  constructor(
    oidcSecurityService: OidcSecurityService,
    private router: Router) { 
    oidcSecurityService.checkAuth().subscribe((loginResponse) => {
      this.isLogged = loginResponse.isAuthenticated;

      loginResponse.userData = jwtDecode(loginResponse.idToken);
      const {email, name: username, sub: id} = loginResponse.userData;
      this.userData = { email: email, username: username, id: id }
      
      if (this.isLogged === false && loginResponse.errorMessage !== undefined) {
        console.error(loginResponse.errorMessage);
      }
    })
  }

  public login(): Promise<boolean> {
    return this.router.navigateByUrl('/login');
  }
  public logout(): Promise<boolean> {
    return this.router.navigateByUrl('/logout');
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
