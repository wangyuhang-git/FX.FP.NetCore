import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalrindexComponent } from './signalrindex.component';

describe('SingalrindexComponent', () => {
  let component: SignalrindexComponent;
  let fixture: ComponentFixture<SignalrindexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignalrindexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignalrindexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
