import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PersonViewModel } from 'src/app/core/models/person-view-model';
import { PersonService } from 'src/app/core/services/person.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html'
})
export class PersonListComponent implements OnInit {
  @Output() selectPerson = new EventEmitter<number>();

  persons: PersonViewModel[] | undefined;
  selectedPersonId: number | null = null;

  constructor(
    private router: Router,
    private personService: PersonService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getList();
  }

  onPersonClick(id: number): void {
    this.selectedPersonId = id;
    this.router.navigate(['/people', id]);

    this.selectPerson.emit(id);
  }

  getList() {
    this.personService.getAll().subscribe(
      (data) => {
        this.persons = data;
      },
      (error) => {
        this.toastr.error('Failed to load data', 'Error');
      }
    );
  }
}
