import { Component, OnDestroy, OnInit } from '@angular/core';
//*********************************************************************** */
// this projectv is created by Wael Gabsi 
// Contact : waelwaelgabsi@gmail.com
// whatsapp : +216 22152879 
// Software engineer 
// University : ESPRIT ENGINEER 
//*********************************************************************** */

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit  , OnDestroy{
//******************* language supported by this website **************************************** */
isRussian : boolean = false ;
isFrench : boolean = false ;
isArabe : boolean = false ;
isChinese : boolean = false ;
isEnglish : boolean = false ;
isGerman : boolean = false ;
isSpanish : boolean = false ;
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










  // this is sthe componenet main object is to handle the main page 
  ngOnDestroy(): void {
  
  }
  ngOnInit(): void {
    const savedLang = localStorage.getItem('preferredLanguage') || 'en';
  this.setLanguage(savedLang);
  
  }



}
