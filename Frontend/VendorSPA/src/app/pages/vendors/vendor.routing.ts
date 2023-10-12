import { Routes, RouterModule } from '@angular/router';
import { VendorFilesComponent } from './components/vendor-files/vendor-files.component';

const routes: Routes = [
  { path: 'files/:userId/:vendorName', component: VendorFilesComponent },

];

export const VendorRoutes = RouterModule.forChild(routes);
