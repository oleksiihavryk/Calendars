import { Injectable } from '@angular/core';

export interface IModal {
  id: string,
  title: string,
  isActive: boolean,
  afterCloseAction(): void;
}
@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private modals: IModal[] = [];

  public createModal(
    id: string, 
    title: string, 
    afterCloseAction: () => void): IModal {
    const modal = {
       id: id, 
       title: title,
       isActive: false,
       afterCloseAction: afterCloseAction 
    };
    this.modals.push(modal);
    return modal;
  }
  public removeModal(id: string) {
    this.modals = this.modals.filter(m => m.id !== id);
  }
  public toggleModal(id: string) {
    const modal = this.modals.find(m => m.id === id);

    if (modal !== undefined) {
      modal.isActive =! modal.isActive;
    }
  }
}
