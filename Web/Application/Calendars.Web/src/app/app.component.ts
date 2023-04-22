import { Component, AfterViewChecked } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewChecked {
  ngAfterViewChecked(): void {
    const headerElement = document.querySelector('header');
    const routerOutletElement = document.querySelector('router-outlet');
    routerOutletElement?.nextElementSibling?.setAttribute('style', `margin-top: ${headerElement?.clientHeight}px;`)
  }
}
