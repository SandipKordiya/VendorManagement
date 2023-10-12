import { NgModule } from '@angular/core';
import { AmbicionesSplashScreenService } from 'src/@ambiciones/services/splash-screen/splash-screen.service';

@NgModule({
  providers: [
    AmbicionesSplashScreenService
]
})
export class AmbicionesSplashScreenModule { 
   /**
     * Constructor
     */
    constructor(private _ambicionesSplashScreenService: AmbicionesSplashScreenService)
    {
    }
}
