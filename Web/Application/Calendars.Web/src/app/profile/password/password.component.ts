import { Component } from '@angular/core';
import { ModalService } from 'src/app/shared/services/modal.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent {
  public featureIsUnavailableModalId: string = 'FeatureIsUnavailableModalId';

  constructor(private modal: ModalService) {}

  public featureUnavailable() {
    this.modal.toggleModal(this.featureIsUnavailableModalId);
  }
}
