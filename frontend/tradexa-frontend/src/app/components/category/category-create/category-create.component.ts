import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.css']
})
export class CategoryCreateComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder, private categoryService: CategoryService, private router: Router) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      englishName: ['', Validators.required],
      arabicName: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.valid) {
      this.categoryService.create(this.form.value).subscribe(() => {
        this.router.navigate(['/categories']);
      });
    }
  }
}
