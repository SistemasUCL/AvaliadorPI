import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PortletGridComponent } from './portlet-grid.component';

describe('PortletGridComponent', () => {
  let component: PortletGridComponent;
  let fixture: ComponentFixture<PortletGridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortletGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortletGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
