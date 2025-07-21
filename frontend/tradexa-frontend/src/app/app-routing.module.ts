import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

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
import { MainLayoutComponent } from './shared/layout/main-layout/main-layout.component';

const routes: Routes = [
   {path: '',
   component: MainLayoutComponent,
   children: [
  { path: '', redirectTo: 'products', pathMatch: 'full' },

  { path: 'products', component: ProductListComponent },
  { path: 'products/create', component: ProductCreateComponent },
  { path: 'products/edit/:id', component: ProductEditComponent },

  { path: 'categories', component: CategoryListComponent },
  { path: 'categories/create', component: CategoryCreateComponent },
  { path: 'categories/edit/:id', component: CategoryEditComponent },

  { path: 'users', component: UserListComponent },
  { path: 'users/create', component: UserCreateComponent },
  { path: 'users/edit/:id', component: UserEditComponent },

  { path: 'invoices', component: InvoiceListComponent },
  { path: 'invoices/create', component: InvoiceCreateComponent },
  { path: 'invoices/edit/:id', component: InvoiceEditComponent },

  { path: 'reports', component: ReportDashboardComponent },
  { path: 'reports/export', component: ReportExportComponent },
]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
