import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})
export class UnauthorizedComponent implements OnInit {
  constructor(private cookieService: CookieService) {
  }

  ngOnInit(): void {
    this.cookieService.deleteAll('/', window.location.hostname);
  }
}
