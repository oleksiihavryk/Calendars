import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, switchMap } from 'rxjs';
import { Event } from 'src/app/shared/domain/event';
import { IResponse, ResourcesHttpClient } from 'src/app/shared/services/resources-http-client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventsService {
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
          environment.resources.url + `/event/id/${id}`,
          'GET', 
          token
        )
      })
    )
  }
  public delete(event: Event): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + `/event/id/${event.id}`,
          'DELETE', 
          token
        )
      })
    )
  }
  public createNew(event: Event): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/event',
          'POST', 
          token,
          event
        )
      })
    )
  }
  public updateByEventId(event: Event): Observable<IResponse> {
    return this.token.pipe(
      switchMap(token => {
        return this.client.makeAuthorizedRequest(
          environment.resources.url + '/event',
          'PUT', 
          token,
          event
        )
      })
    )
  }
}
