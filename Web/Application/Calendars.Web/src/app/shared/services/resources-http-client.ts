import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from 'rxjs';

export interface IResponse {
    isSuccess: boolean,
    statusCode: number,
    result: {} | null,
    messages: string[],
}
@Injectable({
    providedIn: 'root'   
})
export class ResourcesHttpClient extends HttpClient {
    public makeAuthorizedRequest(
        url: string,
        method: 'GET' | 'POST' | 'PUT' | 'DELETE',
        token: string,
        body: {} | undefined | null = undefined,
    ): Observable<IResponse> {
        return this.request<IResponse>(method, url, {
            body: body,
            responseType: 'json',
            headers: {
                'Authorization': 'Bearer '+token
            }
        });
    }
}
