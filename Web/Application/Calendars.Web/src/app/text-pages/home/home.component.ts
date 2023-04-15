import { Component } from '@angular/core';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public startUnavailableModalId: string = 'startUnavailable';

  constructor(public modalService: ModalService) {
  }

  public startUnavailable(): boolean {
    this.modalService.toggleModal(this.startUnavailableModalId);
    return false;
  }
}
