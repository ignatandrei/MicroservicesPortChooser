import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MPCv1Component } from './mpcv1.component';

describe('MPCv1Component', () => {
  let component: MPCv1Component;
  let fixture: ComponentFixture<MPCv1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MPCv1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MPCv1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
