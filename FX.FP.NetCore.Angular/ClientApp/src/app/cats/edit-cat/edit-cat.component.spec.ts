import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCatComponent } from './edit-cat.component';

describe('EditCatComponent', () => {
  let component: EditCatComponent;
  let fixture: ComponentFixture<EditCatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
