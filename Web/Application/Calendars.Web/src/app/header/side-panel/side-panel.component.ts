import { Component, Input } from '@angular/core';
import { NavigationPanelService } from '../services/navigation-panel.service';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-side-panel',
  templateUrl: './side-panel.component.html',
  styleUrls: ['./side-panel.component.css']
})
export class SidePanelComponent {
  public loginUnavailableModalId: string = 'loginUnavailable';

  constructor(
    public sidePanel: NavigationPanelService,
    public modalService: ModalService) {
  }

  public invokeLoginUnavailableModal() : boolean {
    this.sidePanel.close();
    this.modalService.toggleModal(this.loginUnavailableModalId);
    return false;
  }
}
