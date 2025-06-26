import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './Components/LandingPage/landing-page/landing-page.component';
import { AuthClientComponent } from './Components/authentication/auth-client/auth-client.component';
import { ClientComponent } from './Components/dashboards/client/client.component';

const routes: Routes = [{ path: '', component: LandingPageComponent },
{ path: 'authentication', component: AuthClientComponent } , 
{ path: 'client', component: ClientComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
