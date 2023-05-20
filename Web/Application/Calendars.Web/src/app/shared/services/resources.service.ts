import { Injectable } from '@angular/core';
import { Observable, mergeMap } from 'rxjs';
import { HttpRequestService, IRequest, IResponse } from './http-request.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class ResourcesService extends HttpRequestService {
  constructor(private oidc: OidcSecurityService) {
    super()
   }

  public authRequest(request: IRequest) : Observable<IResponse> {
    const authRequest = this.oidc.getAccessToken()
      .pipe(
        mergeMap(token => {
          if (request.headers === undefined) {
            request.headers = {
              Authentication: 'Bearer ' + token
            }
          }
          return this.request(request);
        })
      );
    return authRequest;
  }
}
