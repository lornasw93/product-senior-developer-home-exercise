import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, BehaviorSubject } from 'rxjs';
import { PersonViewModel } from '../models/person-view-model';
import { format } from 'date-fns';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private peopleSubject = new BehaviorSubject<PersonViewModel[]>([]);
  people$ = this.peopleSubject.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  get(id: number): Observable<PersonViewModel> {
    return this.http.get<PersonViewModel>(`${this.baseUrl}api/person/${id}`).pipe(
      map((p) => {
        p.dateOfBirth = format(p.dateOfBirth, 'yyyy-MM-dd');

        return p;
      }));
  }

  getAll(): Observable<PersonViewModel[]> {
    return this.http.get<PersonViewModel[]>(`${this.baseUrl}api/person`);
  }

  getAllPeople(): void {
    this.http.get<PersonViewModel[]>(`${this.baseUrl}api/person`).subscribe(data => this.peopleSubject.next(data));
  }

  create(person: PersonViewModel): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}api/person`, person);
  }

  update(id: number, person: PersonViewModel): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}api/person/${id}`, person).pipe(
      tap(() => {
        const currentPeople = this.peopleSubject.getValue();
        const index = currentPeople.findIndex(p => p.id === person.id);
        if (index !== -1) {
          currentPeople[index] = { ...person };
          this.peopleSubject.next([...currentPeople]);
        }
      })
    );
  }
}
