import { TestBed } from '@angular/core/testing';

import { GetMediaService } from './get-media.service';

describe('GetMediaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GetMediaService = TestBed.get(GetMediaService);
    expect(service).toBeTruthy();
  });
});
