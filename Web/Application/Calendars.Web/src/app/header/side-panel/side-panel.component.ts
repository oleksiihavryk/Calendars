import { Component } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';
import { AuthorizeService } from 'src/app/authentication/services/authorize.service';

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.css']
})
export class SidePanelComponent {
  constructor(
    public sidePanel: NavigationPanelService,
    public auth: AuthorizeService) {
  }
}
