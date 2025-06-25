import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  Renderer2,
  ElementRef,
  ViewChild
} from '@angular/core';

@Component({
  selector: 'app-auth-client',
  templateUrl: './auth-client.component.html',
  styleUrls: ['./auth-client.component.css']
})
export class AuthClientComponent implements OnInit, OnDestroy, AfterViewInit {

  @ViewChild('formContainer') formContainer!: ElementRef;

  constructor(private renderer: Renderer2) {}

  //*************** LANGUAGE *******************/
  isRussian = false;
  isFrench = false;
  isArabe = false;
  isChinese = false;
  isEnglish = false;
  isGerman = false;
  isSpanish = false;

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

  ngOnInit(): void {
    const savedLang = localStorage.getItem('preferredLanguage') || 'en';
    this.setLanguage(savedLang);
  }

  ngAfterViewInit(): void {
    // Password toggle
    this.renderer.listen(document, 'click', (event: Event) => {
      const target = event.target as HTMLElement;
      const toggle = target.closest('.password-toggle');
      if (toggle) {
        const input = (toggle.previousElementSibling as HTMLInputElement);
        const icon = toggle.querySelector('.password-icon')!;
        if (input.type === 'password') {
          input.type = 'text';
          icon.innerHTML = '<path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24"/><line x1="1" y1="1" x2="23" y2="23"/>';
        } else {
          input.type = 'password';
          icon.innerHTML = '<path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/>';
        }
      }
    });

    // Focus and blur styling
    const inputs = document.querySelectorAll('.form-input');
    inputs.forEach(input => {
      input.addEventListener('focus', () => {
        input.parentElement?.classList.add('focused');
      });
      input.addEventListener('blur', () => {
        input.parentElement?.classList.remove('focused');
      });
    });
  }

  flipForm(): void {
    if (this.formContainer) {
      this.renderer.addClass(this.formContainer.nativeElement, 'flipped');
    }
  }

  unflipForm(): void {
    if (this.formContainer) {
      this.renderer.removeClass(this.formContainer.nativeElement, 'flipped');
    }
  }

  ngOnDestroy(): void {}
}
