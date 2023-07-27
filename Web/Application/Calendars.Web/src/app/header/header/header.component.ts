import { Component } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(
    public sidePanel: NavigationPanelService,
    public auth: AuthorizeService) {
  }
}
