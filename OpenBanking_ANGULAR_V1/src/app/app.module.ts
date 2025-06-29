import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './Components/LandingPage/landing-page/landing-page.component';
import { AuthClientComponent } from './Components/authentication/auth-client/auth-client.component';
import { ClientComponent } from './Components/dashboards/client/client.component';
import { FirstPageComponent } from './Components/ApiExplorer/first-page/first-page.component';
import { DescReqResComponent } from './Components/ApiExplorer/desc-req-res/desc-req-res.component';
//import { LandingPageComponent } from './components/LandingPage/landing-page/landing-page.component';


@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    AuthClientComponent,
    ClientComponent,
    FirstPageComponent,
    DescReqResComponent


  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
