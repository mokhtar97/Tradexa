import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  selectedLang = 'en';

  constructor(public translate: TranslateService, public themeService: ThemeService) {
    this.selectedLang = translate.currentLang || 'en';
  }

  switchLang(lang: string) {
    this.selectedLang = lang;
    this.translate.use(lang);
    document.dir = lang === 'ar' ? 'rtl' : 'ltr';
  }
}
