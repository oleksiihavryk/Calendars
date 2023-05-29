import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, map, switchMap } from 'rxjs';

import { Calendar } from 'src/app/shared/domain/calendar';
import { ResourcesHttpClient, IResponse } from 'src/app/shared/services/resources-http-client';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalendarsService {
  private get userId(): Observable<string> {
    return this.oidc.getUserData().pipe(
       map(data => data.sub)
    );
  }
  private get token(): Observable<string> {
    return this.oidc.getAccessToken();
  }

  constructor(
    private client: ResourcesHttpClient,
    private oidc: OidcSecurityService) { }

  public getAll(): Observable<IResponse> {
    return this.userId.pipe(
      switchMap(userId => {
        return this.token.pipe(
          map(token => {
            return { token: token, userId: userId }
          })
        )
      }),
      switchMap(userIdAndToken => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/calendar/user-id/${userIdAndToken.userId}`,
          'GET', 
          userIdAndToken.token
        )
      })
    )
  }
  public getById(id: string): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/calendar/id/${id}`,
          'GET', 
          token
        )
      })
    )
  }
  public delete(calendar: Calendar): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/calendar/id/${calendar.id}`,
          'DELETE', 
          token
        )
      })
    )
  }
  public createNew(calendar: Calendar): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/calendar',
          'POST', 
          token,
          calendar
        )
      })
    )
  }
  public updateByCalendarId(calendar: Calendar): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/calendar',
          'PUT', 
          token,
          calendar
        )
      })
    )
  }
}
