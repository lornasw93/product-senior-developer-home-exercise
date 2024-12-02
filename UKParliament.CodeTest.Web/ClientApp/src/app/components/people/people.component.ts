import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonViewModel } from 'src/app/core/models/person-view-model';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html'
})
export class PeopleComponent {
  public router: Router;
  public activatedRoute: ActivatedRoute;

  people: PersonViewModel[] | undefined;
  selectedPersonId: number | null = null;
  toggleCreate = false;

  constructor(_router: Router,
    _activatedRoute: ActivatedRoute) {
    this.router = _router;
    this.activatedRoute = _activatedRoute;

    if (this.activatedRoute.snapshot.params.id) {
      this.selectedPersonId = this.activatedRoute.snapshot.params.id;
    }
  }

  onSelectPerson(id: number): void {
    this.selectedPersonId = id;
    this.toggleCreate = false;
  }

  toggleCreateMode(): void {
    this.toggleCreate = !this.toggleCreate;
    this.selectedPersonId = null;
    this.router.navigate(['/people']);
  }
}
