import { Component, ElementRef } from '@angular/core';
import { PageScrollService } from 'ngx-page-scroll-core';

@Component({
  selector: 'app-months-navigator',
  templateUrl: './months-navigator.component.html',
  styleUrls: ['./months-navigator.component.css']
})
export class MonthsNavigatorComponent {
  public hidden: boolean = false;
  public buttonsHidden: boolean = false;

  constructor(
    private scrollService: PageScrollService, 
    private el: ElementRef) { }

  public scroll(to: string): void {
    this.scrollService.scroll({
      scrollOffset: 75,
      duration: 1,
      document: (this.el.nativeElement as HTMLElement).ownerDocument,
      scrollTarget: to
    })
  }
  public hidePanel() {
    this.hidden = true;
    this.buttonsHidden = false;
  }
  public showPanel() {
    this.hidden = false;
    this.buttonsHidden = false;
  }
  public showButtons() {
    if (this.hidden) {
      this.buttonsHidden = false;
    }
  }
  public hideButtons() {
    if (this.hidden) {
      this.buttonsHidden = true;
    }
  }
}
