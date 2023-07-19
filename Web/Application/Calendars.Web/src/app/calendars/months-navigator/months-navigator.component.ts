import { Component, ElementRef } from '@angular/core';
import { PageScrollService } from 'ngx-page-scroll-core';
import $ from "jquery";

@Component({
  selector: 'app-months-navigator',
  templateUrl: './months-navigator.component.html',
  styleUrls: ['./months-navigator.component.css']
})
export class MonthsNavigatorComponent {
  public panelHidden: boolean = false;
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

  public hidePanel(panel: HTMLElement) {
    $(panel).slideUp(125);
    this.panelHidden = true;
  }
  public showPanel(panel: HTMLElement) {
    $(panel).slideDown(125);
    this.panelHidden = false;
  }
  public showButtons(buttons: HTMLElement) {
    this.buttonsHidden = false;
    if (this.panelHidden) {
      $(buttons).stop().slideDown(250, () => {
        this.buttonsHidden = false;
      });
      console.log("buttons is show!")
    }
  }
  public hideButtons(buttons: HTMLElement) {
    if (this.panelHidden && this.buttonsHidden === false) {
      $(buttons).stop().slideUp(250, () => {
        this.buttonsHidden = true;
      });
      console.log("buttons is hidden!")
    }
  }
}
