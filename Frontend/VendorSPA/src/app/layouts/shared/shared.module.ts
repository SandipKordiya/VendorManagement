import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { ClickOutsideModule } from 'ng-click-outside';

import { TopbarComponent } from './topbar/topbar.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RightsidebarComponent } from './rightsidebar/rightsidebar.component';
import { HorizontaltopbarComponent } from './horizontaltopbar/horizontaltopbar.component';
import { HorizontalnavbarComponent } from './horizontalnavbar/horizontalnavbar.component';

@NgModule({
  declarations: [TopbarComponent, FooterComponent, SidebarComponent, RightsidebarComponent, HorizontaltopbarComponent, HorizontalnavbarComponent],
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    NgbDropdownModule,
    ClickOutsideModule,
    RouterModule
  ],
  exports: [TopbarComponent, FooterComponent, SidebarComponent, RightsidebarComponent, HorizontaltopbarComponent, HorizontalnavbarComponent],
  providers: [],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class SharedModule { }
