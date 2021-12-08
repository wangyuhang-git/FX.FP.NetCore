import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JichuComponent } from './jichu.component';

describe('JichuComponent', () => {
  let component: JichuComponent;
  let fixture: ComponentFixture<JichuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JichuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JichuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
