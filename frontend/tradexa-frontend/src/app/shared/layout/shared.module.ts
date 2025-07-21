import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TranslateModule } from '@ngx-translate/core';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

  
@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    MainLayoutComponent
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    BrowserModule,
    RouterModule,  
    MatCheckboxModule,
    MatSlideToggleModule,
    TranslateModule,
    MatSelectModule,
    MatOptionModule
  ],
  exports: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    MainLayoutComponent
  ]
})
export class SharedModule { }
