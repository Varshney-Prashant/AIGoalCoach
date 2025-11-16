// loader.component.ts
import { Component } from '@angular/core';
import { LoaderService } from './loader.service';

@Component({
  selector: 'app-loader',
  template: `
    <div class="loader-backdrop" *ngIf="loading$ | async">
      <div class="loader"></div>
    </div>
  `
})
export class LoaderComponent {
  loading$ = this.loaderService.loading$;
  constructor(private loaderService: LoaderService) {}
}
