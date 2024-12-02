import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeopleComponent } from './people.component';

describe('PeopleComponent', () => {
  let component: PeopleComponent;
  let fixture: ComponentFixture<PeopleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PeopleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PeopleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle create mode', () => {
    component.toggleCreateMode();
    expect(component.toggleCreate).toBeTrue();
    expect(component.selectedPersonId).toBeNull();
  });

  it('should select a person', () => {
    const personId = 1;
    component.onSelectPerson(personId);
    expect(component.selectedPersonId).toBe(personId);
    expect(component.toggleCreate).toBeFalse();
  });

  it('should set selectedPersonId from route', () => {
    const personId = 1;
    spyOnProperty(component.activatedRoute.snapshot, 'params', 'get').and.returnValue({ id: personId });
    component = new PeopleComponent(component.router, component.activatedRoute);
    expect(component.selectedPersonId).toBe(personId);
  });

  it('should navigate to /people', () => {
    const routerSpy = spyOn(component.router, 'navigate');
    component.toggleCreateMode();
    expect(routerSpy).toHaveBeenCalledWith(['/people']);
  });
});
