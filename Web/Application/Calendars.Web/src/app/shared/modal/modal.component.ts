import { Component, ElementRef, Input, OnInit, OnDestroy } from '@angular/core';
import { IModal, ModalService } from '../services/modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit, OnDestroy {
  @Input() public id: string = '';
  @Input() public title: string = '';
  public modal: IModal = {isActive: false, id: '', title: ''};

  constructor(private ref: ElementRef, private modalService: ModalService) {
  } 

  ngOnInit(): void {
    this.replaceElement();
    this.modal = this.modalService.createModal(this.id, this.title);
  }
  ngOnDestroy(): void {
    this.modalService.removeModal(this.id);
  }

  public toggle(): boolean {
    this.modalService.toggleModal(this.modal.id);
    return false;
  }

  private replaceElement() {
    const el = this.ref.nativeElement as HTMLElement;

    if (el !== undefined)  { 
      el.remove();
      document.body.appendChild(el);
    }
  }
}
