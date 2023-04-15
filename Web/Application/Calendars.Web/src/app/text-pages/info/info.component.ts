import { Component } from '@angular/core';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent {
  public startUnavailableModalId: string = 'startUnavailable';

  constructor(public modalService: ModalService) {
  }

  public startUnavailable(): boolean {
    this.modalService.toggleModal(this.startUnavailableModalId);
    return false;
  }
}
