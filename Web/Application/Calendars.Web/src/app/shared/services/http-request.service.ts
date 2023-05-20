import { Injectable } from "@angular/core";
import { Observable, from, map, switchMap } from "rxjs";

export interface IRequest {
  url: string,
  jsonArguments: {} | undefined,
  method: 'POST' | 'GET' | 'PUT' | 'PATCH' | 'DELETE', 
  headers: {} | undefined
}
export interface IResponse {
  isSuccess: boolean,
  statusCode: number,
  result: {} | null,
  messages: string[]
}
@Injectable({
  providedIn: 'root'
})
export abstract class HttpRequestService {
  public request(request: IRequest): Observable<IResponse> {
    return from(fetch(request.url, {
        method: request.method,
        body: JSON.stringify(request.jsonArguments),
        headers: request.headers
      }))
      .pipe(
        switchMap((fetchResponse, _) => {
          return fetchResponse.json()
        }),
        map(function(rawResponse): IResponse {
          const response: IResponse = {
            isSuccess: rawResponse.isSuccess,
            statusCode: rawResponse.statusCode,
            result: rawResponse.result,
            messages: rawResponse.messages
          }
          return response;
        })
      );
  }
}
