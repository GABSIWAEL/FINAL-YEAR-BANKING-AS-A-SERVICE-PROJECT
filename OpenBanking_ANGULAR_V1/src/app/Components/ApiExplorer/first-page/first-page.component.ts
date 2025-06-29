import { Component, OnDestroy, OnInit } from '@angular/core';
//*********************************************************************** */
// this projectv is created by Wael Gabsi 
// Contact : waelwaelgabsi@gmail.com
// whatsapp : +216 22152879 
// Software engineer 
// University : ESPRIT ENGINEER 
//*********************************************************************** */
@Component({
  selector: 'app-first-page',
  templateUrl: './first-page.component.html',
  styleUrls: ['./first-page.component.css']
})
export class FirstPageComponent implements OnInit, OnDestroy {
 showSplash = true;
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





 apiData: Record<string, { description: string; request: string; response: string }> = {
    'CardAttributeDefinition': {
      description: 'Retrieve metadata about card attributes such as card type, expiration, and status.',
      request: `GET /api/cards/attribute-definition`,
      response: `{
  "attributes": [
    {"id": 1, "name": "cardType"},
    {"id": 2, "name": "expirationDate"},
    {"id": 3, "name": "status"}
  ]
}`
    },
    'CardById': {
      description: 'Fetch details for a specific card by its unique ID.',
      request: `GET /api/cards/{cardId}`,
      response: `{
  "cardId": "12345",
  "cardType": "Credit",
  "expirationDate": "2026-12-31",
  "status": "Active"
}`
    },
    'CardsForTheCurrentUser': {
      description: 'List all cards associated with the current authenticated user.',
      request: `GET /api/cards/user`,
      response: `[
  {
    "cardId": "12345",
    "cardType": "Debit",
    "status": "Active"
  },
  {
    "cardId": "67890",
    "cardType": "Credit",
    "status": "Blocked"
  }
]`
    },
    'CardsForTheSpecifiedBank': {
      description: 'Retrieve cards issued by a specified bank.',
      request: `GET /api/cards/bank/{bankId}`,
      response: `[
  {
    "cardId": "abc123",
    "cardType": "Credit",
    "bankId": "bank001",
    "status": "Active"
  }
]`
    },
    'StatusOfCreditCardOrder': {
      description: 'Check the current status of a credit card order.',
      request: `GET /api/cards/order/{orderId}/status`,
      response: `{
  "orderId": "order789",
  "status": "Processing"
}`
    },
  };


 expandedSections = {
    account: true,
    atm: false,
    branch: false,
    card: false,
  };

  toggleSection(section: keyof typeof this.expandedSections) {
    this.expandedSections[section] = !this.expandedSections[section];
  }

  // this is sthe componenet main object is to handle the main page 
  ngOnDestroy(): void {

  }
  ngOnInit(): void {

    
    const savedLang = localStorage.getItem('preferredLanguage') || 'en';
    this.setLanguage(savedLang);

    setTimeout(() => {
      this.showSplash = false;
    }, 7000);

  }
   selectedApiItem: string | null = null;
selectApiItem(apiItem: string) {
    this.selectedApiItem = apiItem;
  }
}
