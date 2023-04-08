import { TestBed } from '@angular/core/testing';

import { NavigationPanelService } from './navigation-panel.service';

describe('NavigationPanelService', () => {
  let service: NavigationPanelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NavigationPanelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
