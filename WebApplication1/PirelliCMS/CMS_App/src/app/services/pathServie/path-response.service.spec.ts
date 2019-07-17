import { TestBed } from '@angular/core/testing';

import { PathResponseService } from './path-response.service';

describe('PathResponseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PathResponseService = TestBed.get(PathResponseService);
    expect(service).toBeTruthy();
  });
});
