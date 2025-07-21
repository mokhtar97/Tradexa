// src/app/components/product/product-list/product-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Product } from 'src/app/models/models';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
})
export class ProductListComponent implements OnInit {
  products: any[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.productService.getAll().subscribe(data => {
      this.products = data;
    });
  }

  deleteProduct(id: string): void {
    if (confirm('Are you sure?')) {
      this.productService.delete(id).subscribe(() => this.load());
    }
  }
}
