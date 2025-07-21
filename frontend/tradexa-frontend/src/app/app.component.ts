import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  {
  constructor(private translate: TranslateService) {
    const defaultLang = localStorage.getItem('lang') || 'en';
    this.switchLang(defaultLang);
  }

  switchLang(lang: string) {
    this.translate.use(lang);
    localStorage.setItem('lang', lang);
    document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';
    document.documentElement.lang = lang;
  }
}
