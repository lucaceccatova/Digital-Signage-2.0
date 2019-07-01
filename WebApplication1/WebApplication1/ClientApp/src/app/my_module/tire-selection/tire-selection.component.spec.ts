import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TireSelectionComponent } from './tire-selection.component';

describe('TireSelectionComponent', () => {
  let component: TireSelectionComponent;
  let fixture: ComponentFixture<TireSelectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TireSelectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TireSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
