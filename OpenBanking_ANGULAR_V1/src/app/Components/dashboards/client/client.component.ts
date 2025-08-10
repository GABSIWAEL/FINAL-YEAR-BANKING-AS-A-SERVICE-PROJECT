import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/user';
import { AuthServiceService } from 'src/app/Services/auth-service.service';
//*********************************************************************** */
// this projectv is created by Wael Gabsi 
// Contact : waelwaelgabsi@gmail.com
// whatsapp : +216 22152879 
// Software engineer 
// University : ESPRIT ENGINEER 
//*********************************************************************** */
@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit, OnDestroy {


  user!: User;  // Declare a user property of type User
  isLoading = true;

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }


  constructor(private authService: AuthServiceService, private router: Router) { }

  userInfo: any = null;


  //******************* language supported by this website **************************************** */
  isRussian: boolean = false;
  isFrench: boolean = false;
  isArabe: boolean = false;
  isChinese: boolean = false;
  isEnglish: boolean = false;
  isGerman: boolean = false;
  isSpanish: boolean = false;
  //************************** Selecting The Preferring Language ******************************** */
  setLanguage(language: string): void {
    this.isRussian = language === 'ru';
    this.isFrench = language === 'fr';
    this.isArabe = language === 'ar';
    this.isChinese = language === 'zh';
    this.isEnglish = language === 'en';
    this.isGerman = language === 'de';
    this.isSpanish = language === 'es';


    localStorage.setItem('preferredLanguage', language);
  }






  getUserInfo(): void {
    this.authService.getUserInfo().subscribe(
      (userData: User) => {
        this.user = userData;
        this.isLoading = false;  // Set loading state to false after data is fetched
      },
      (error) => {
        console.error('Error fetching user info:', error);
        this.isLoading = false;
      }
    );
  }



  // this is sthe componenet main object is to handle the main page 
  ngOnDestroy(): void {

  }
  ngOnInit(): void {
    const savedLang = localStorage.getItem('preferredLanguage') || 'en';
    this.setLanguage(savedLang);
    if (this.authService.isLoggedIn()) {
      this.getUserInfo();
    }
  }
}




