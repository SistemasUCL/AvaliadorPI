import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AvalidoresComponent } from './avalidores.component';

describe('AvalidoresComponent', () => {
  let component: AvalidoresComponent;
  let fixture: ComponentFixture<AvalidoresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AvalidoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AvalidoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
