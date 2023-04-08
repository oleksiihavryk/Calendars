import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationPanelService {
  private enabled: boolean = false;

  public get isOpen() {
    return this.enabled;
  }
  public get isClosed() {
    return !this.enabled;
  }
  
  constructor() { }
  
  public open() {
    this.enabled = true;
  }
  public close() {
    this.enabled = false;
  }
  public changeState() {
    if (this.isOpen) {
      this.close();
    } else {
      this.open();
    }
  }
}
