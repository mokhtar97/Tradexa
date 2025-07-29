// src/app/components/product/product-list/product-list.component.ts
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Directionality } from '@angular/cdk/bidi';
import { MatSort } from '@angular/material/sort';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  //styleUrls: './product-list.component.css',
  encapsulation: ViewEncapsulation.None
})
export class ProductListComponent implements OnInit {
  displayedColumns: string[] = [ 'arabicName', 'categoryName', 'price', 'stock', 'actions'];//'englishName',
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  totalCount = 0;
  pageSize = 10;
  currentPage = 0;
  searchText = '';
  isLoading = false;


  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.fetchProducts();
      this.currentPage = 0;
      this.fetchProducts();
  }

  fetchProducts(): void {
    this.isLoading = true;

    const params = {
      page: this.currentPage + 1,
      pageSize: this.pageSize,
      search: this.searchText
    };

    this.productService.getProducts(params).subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res.items);
        this.totalCount = res.totalCount;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }


  applyFilter(event: Event): void {
    const value = (event.target as HTMLInputElement).value.trim();
    this.fetchProducts();
  }

  clearSearch(): void {
    this.searchText = '';
    this.currentPage = 0;
    this.fetchProducts();
  }

  deleteProduct(id: string): void {
    if (confirm('Are you sure you want to delete this product?')) {
      // this.productService.deleteProduct(id).subscribe(() => {
      //   this.fetchProducts();
      // });
    }
  }

  onPageChange(event: PageEvent): void {
  this.pageSize = event.pageSize;
  this.currentPage = event.pageIndex;
  this.fetchProducts();
}
}