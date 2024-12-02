import { DepartmentViewModel } from "./department-view-model";

export interface PersonViewModel {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  initials: string;

  // departmentId: number;
  // department: DepartmentViewModel;
}
