import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagetitleComponent } from './pagetitle/pagetitle.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PaginationComponent } from './pagination/pagination.component';

@NgModule({
  declarations: [PagetitleComponent,PaginationComponent],
  imports: [
    CommonModule,
    NgbPaginationModule,
    
  ],
  exports: [
    NgbPaginationModule,
    PagetitleComponent,
    PaginationComponent
  ]
})
export class UiModule { }
