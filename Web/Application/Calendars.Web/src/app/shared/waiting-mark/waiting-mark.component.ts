import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-waiting-mark',
  templateUrl: './waiting-mark.component.html',
  styleUrls: ['./waiting-mark.component.css']
})
export class WaitingMarkComponent implements OnInit, OnDestroy {
  private el: HTMLElement;
  private sub: Subscription | undefined;

  constructor(el: ElementRef) {
    this.el = el.nativeElement as HTMLElement;
    if (this.el === null)
      throw new Error('Initialization error...');
  }

  ngOnInit(): void {
    this.el.textContent = '.';
    this.sub = interval(1000).subscribe(n => {
      if (n % 3 !== 0) {
        this.el.textContent += '.';
      } else {
        this.el.textContent = '.';
      }
    });
  }
  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }
}
