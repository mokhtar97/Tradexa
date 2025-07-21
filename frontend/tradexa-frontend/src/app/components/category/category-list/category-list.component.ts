import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories: any[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.categoryService.getAll().subscribe(data => this.categories = data);
  }

  deleteCategory(id: string) {
    if (confirm('Are you sure?')) {
      this.categoryService.delete(id).subscribe(() => this.load());
    }
  }
}
