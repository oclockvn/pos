import { TestBed } from '@angular/core/testing';

import { AuthorizeGuard } from './authorize.guard';

describe('AuthorizeGuard', () => {
  let guard: AuthorizeGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthorizeGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
