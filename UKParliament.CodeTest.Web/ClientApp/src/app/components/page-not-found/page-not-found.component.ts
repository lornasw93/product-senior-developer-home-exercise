import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html'
})
export class PageNotFoundComponent {
  public router: Router;

  constructor(_router: Router) {
    this.router = _router;
  }

  goDirectory(): void {
    this.router.navigate(['/people']);
  }
}
