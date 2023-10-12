import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VendorFilesComponent } from './components/vendor-files/vendor-files.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { VendorRoutes } from './vendor.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileUploaderComponent } from './components/file-uploader/file-uploader.component';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { TableLoaderModule } from '../components/table-loader/table-loader.module';

@NgModule({
  imports: [    
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    VendorRoutes,
    NgbAlertModule,
    TableLoaderModule
  ],
  declarations: [VendorFilesComponent,FileUploaderComponent]
})
export class VendorsModule { }
