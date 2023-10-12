import { NgModule, Optional, SkipSelf } from '@angular/core';
// import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { AmbicionesSplashScreenModule } from 'src/@ambiciones/services/splash-screen/splash-screen.module';

@NgModule({
  imports: [
    AmbicionesSplashScreenModule
  ],
//   providers: [
//     {
//         // Use the 'fill' appearance on Angular Material form fields by default
//         provide : MAT_FORM_FIELD_DEFAULT_OPTIONS,
//         useValue: {
//             appearance: 'fill'
//         }
//     }
// ]
})
export class AmbicionesModule { 
  /**
     * Constructor
     */
   constructor(@Optional() @SkipSelf() parentModule?: AmbicionesModule)
   {
       if ( parentModule )
       {
           throw new Error('AmbicionesModule has already been loaded. Import this module in the AppModule only!');
       }
   }
}
