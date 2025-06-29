import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DescReqResComponent } from './desc-req-res.component';

describe('DescReqResComponent', () => {
  let component: DescReqResComponent;
  let fixture: ComponentFixture<DescReqResComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DescReqResComponent]
    });
    fixture = TestBed.createComponent(DescReqResComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
