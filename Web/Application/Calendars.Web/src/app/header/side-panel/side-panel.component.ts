import { Component, Input } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.css']
})
export class SidePanelComponent {
  constructor(public sidePanel: NavigationPanelService) {
  }
}
