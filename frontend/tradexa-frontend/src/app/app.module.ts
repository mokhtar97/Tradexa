import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { ProductListComponent } from './components/product/product-list/product-list.component';
import { ProductCreateComponent } from './components/product/product-create/product-create.component';
import { ProductEditComponent } from './components/product/product-edit/product-edit.component';

import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryCreateComponent } from './components/category/category-create/category-create.component';
import { CategoryEditComponent } from './components/category/category-edit/category-edit.component';

import { UserListComponent } from './components/user/user-list/user-list.component';
import { UserCreateComponent } from './components/user/user-create/user-create.component';
import { UserEditComponent } from './components/user/user-edit/user-edit.component';

import { InvoiceListComponent } from './components/invoice/invoice-list/invoice-list.component';
import { InvoiceCreateComponent } from './components/invoice/invoice-create/invoice-create.component';
import { InvoiceEditComponent } from './components/invoice/invoice-edit/invoice-edit.component';

import { ReportDashboardComponent } from './components/report/report-dashboard/report-dashboard.component';
import { ReportExportComponent } from './components/report/report-export/report-export.component';
import { SharedModule } from './shared/layout/shared.module';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,

    // Product
    ProductListComponent,
    ProductCreateComponent,
    ProductEditComponent,

    // Category
    CategoryListComponent,
    CategoryCreateComponent,
    CategoryEditComponent,

    // User
    UserListComponent,
    UserCreateComponent,
    UserEditComponent,

    // Invoice
    InvoiceListComponent,
    InvoiceCreateComponent,
    InvoiceEditComponent,

    // Report
    ReportDashboardComponent,
    ReportExportComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    TranslateModule.forRoot({
          loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient]
          }
        }),
    // Angular Material
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatTableModule,
    MatSelectModule,
    MatCardModule,
    MatSnackBarModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
