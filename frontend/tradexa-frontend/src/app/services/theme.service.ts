import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private themeKey = 'theme';

  constructor() {
    const saved = localStorage.getItem(this.themeKey);
    const theme: 'light' | 'dark' = saved === 'dark' ? 'dark' : 'light';
    this.setTheme(theme);
  }

  setTheme(theme: 'light' | 'dark') {
    document.body.classList.remove('light', 'dark');
    document.body.classList.add(theme);
    localStorage.setItem(this.themeKey, theme);
  }

  toggleTheme() {
    const next = this.getTheme() === 'light' ? 'dark' : 'light';
    this.setTheme(next);
  }

  getTheme(): 'light' | 'dark' {
    return (localStorage.getItem(this.themeKey) as 'light' | 'dark') || 'light';
  }
}
