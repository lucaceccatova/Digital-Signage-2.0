import { TestBed } from '@angular/core/testing';

import { ServerListnerService } from './server-listner.service';

describe('ServerListnerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ServerListnerService = TestBed.get(ServerListnerService);
    expect(service).toBeTruthy();
  });
});
