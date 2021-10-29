import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingalrindexComponent } from './singalrindex.component';

describe('SingalrindexComponent', () => {
  let component: SingalrindexComponent;
  let fixture: ComponentFixture<SingalrindexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingalrindexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingalrindexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
