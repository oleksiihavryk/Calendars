import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { switchMap } from 'rxjs';
import { User } from 'src/app/shared/domain/user';
import { ResourcesHttpClient } from 'src/app/shared/services/resources-http-client';

import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private client: ResourcesHttpClient,
    private oidc: OidcSecurityService) { }

  public update(user: User) {
    return this.oidc.getAccessToken().pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/user', 'PUT', token, user);
      })
    )
  }
}
