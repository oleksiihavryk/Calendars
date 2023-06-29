import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, map, switchMap } from 'rxjs';
import { Day } from 'src/app/shared/domain/day';
import { IResponse, ResourcesHttpClient } from 'src/app/shared/services/resources-http-client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DaysService {
  private get token(): Observable<string> {
    return this.oidc.getAccessToken();
  }

  constructor(
    private client: ResourcesHttpClient,
    private oidc: OidcSecurityService) { }

  public getById(id: string): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/day/id/${id}`,
          'GET', 
          token
        )
      })
    )
  }
  public delete(day: Day): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/day/id/${day.id}`,
          'DELETE', 
          token
        )
      })
    )
  }
  public createNew(day: Day): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/day',
          'POST', 
          token,
          day
        )
      })
    )
  }
  public updateByDayId(day: Day): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/day',
          'PUT', 
          token,
          day
        )
      })
    )
  }
}
