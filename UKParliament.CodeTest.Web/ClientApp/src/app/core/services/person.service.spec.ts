import { TestBed } from "@angular/core/testing";
import { PersonService } from "./person.service";

describe('PersonService', () => {
  let service: PersonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PersonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should have a method called getAll', () => {
    expect(service.getAll).toBeTruthy();
  });

  it('should have a method called get', () => {
    expect(service.get).toBeTruthy();
  });

  it('should have a method called create', () => {
    expect(service.create).toBeTruthy();
  });

  it('should have a method called update', () => {
    expect(service.update).toBeTruthy();
  });

  it('should have a method called getAllPeople', () => {
    expect(service.getAllPeople).toBeTruthy();
  }); 
});