import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './Components/LandingPage/landing-page/landing-page.component';
import { AuthClientComponent } from './Components/authentication/auth-client/auth-client.component';

const routes: Routes = [{ path: '', component: LandingPageComponent },
{ path: 'authentication', component: AuthClientComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
