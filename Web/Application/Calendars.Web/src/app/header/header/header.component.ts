import { Component } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(public sidePanel: NavigationPanelService) {
  }
}
