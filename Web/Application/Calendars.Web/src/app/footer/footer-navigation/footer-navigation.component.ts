import { Component } from '@angular/core';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';

@Component({
  selector: 'app-footer-navigation',
  templateUrl: './footer-navigation.component.html',
  styleUrls: ['./footer-navigation.component.css']
})
export class FooterNavigationComponent { 
  constructor(public authorize: AuthorizeService) {}
}
