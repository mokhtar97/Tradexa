// src/app/components/invoice/invoice-list/invoice-list.component.ts

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceService } from 'src/app/services/invoice.service';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {
  invoices: any[] = [];
  isLoading = false;

  constructor(
    private invoiceService: InvoiceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(): void {
    this.isLoading = true;
    this.invoiceService.getAll().subscribe({
      next: (res) => {
        this.invoices = res;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  deleteInvoice(id: string): void {
    if (confirm('Are you sure you want to delete this invoice?')) {
      this.invoiceService.delete(id).subscribe(() => this.loadInvoices());
    }
  }

  exportPdf(id: string): void {
    // this.invoiceService.exportPdf(id).subscribe((blob) => {
    //   const url = window.URL.createObjectURL(blob);
    //   const a = document.createElement('a');
    //   a.href = url;
    //   a.download = `invoice-${id}.pdf`;
    //   a.click();
    //   window.URL.revokeObjectURL(url);
    // });
  }
}
