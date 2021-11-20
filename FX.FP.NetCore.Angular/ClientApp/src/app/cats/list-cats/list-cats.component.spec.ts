import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCatsComponent } from './list-cats.component';

describe('ListCatsComponent', () => {
  let component: ListCatsComponent;
  let fixture: ComponentFixture<ListCatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
