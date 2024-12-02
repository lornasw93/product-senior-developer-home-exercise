import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { parseISO } from 'date-fns';
import { ToastrService } from 'ngx-toastr';
import { DepartmentViewModel } from 'src/app/core/models/department-view-model';
import { DepartmentService } from 'src/app/core/services/department.service';
import { PersonService } from 'src/app/core/services/person.service';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html'
})
export class PersonFormComponent implements OnChanges {
  @Input() id: number | null = null;

  personForm: FormGroup = this.fb.group({
    title: [{ value: '', disabled: true }, Validators.required],
    firstName: [{ value: '', disabled: true }, Validators.required],
    lastName: [{ value: '', disabled: true }, Validators.required],
    dateOfBirth: [{ value: '', disabled: true }, Validators.required],
    email: [{ value: '', disabled: true }, Validators.required],
    departmentId: [{ value: '', disabled: true }, Validators.required],
  });

  departments: DepartmentViewModel[] | undefined;
  isEditMode = false;
  isCreateMode = false;

  constructor(private fb: FormBuilder,
    private personService: PersonService,
    private departmentService: DepartmentService,
    private toastr: ToastrService
  ) {
    this.getAllDepartments();

    if (!this.id) { 
      this.personForm.enable();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['id'] && this.id) { 
      this.loadPersonData(this.id);
    }
  }

  loadPersonData(id: number): void {
    this.personService.get(id).subscribe(
      (data) => {
        this.personForm.patchValue(data);
      },
      (error) => {
        this.toastr.error('Failed to load data', 'Error');
      }
    );
  }

  toggleEditMode(): void {
    this.isEditMode = !this.isEditMode;
    this.isEditMode ? this.personForm.enable() : this.personForm.disable();
  }

  create(): void {
    if (this.personForm.valid) {
      this.personForm.value.dateOfBirth = parseISO(this.personForm.value.dateOfBirth);

      this.personService.create(this.personForm.value).subscribe(
        () => {
          this.toastr.success(`Successfully added ${this.personForm.value.firstName}!`, 'Success');
        },
        (error) => {
          this.toastr.error('Failed to create', 'Error');
        }
      );
    }
  }

  update(): void {
    if (this.personForm.valid && this.id) {
      this.personService.update(this.id, this.personForm.value).subscribe(
        () => {
          this.toastr.success(`Successfully updated ${this.personForm.value.firstName}!`, 'Success');
          this.toggleEditMode();
        },
        (error) => {
          this.toastr.error('Failed to update', 'Error');
        }
      );
    }
  }

  getAllDepartments(): void {
    this.departmentService.getAll().subscribe(
      (data) => {
        this.departments = data;
      },
      (error) => {
        this.toastr.error('Failed to retrieve departments', 'Error');
      }
    );
  }
}
