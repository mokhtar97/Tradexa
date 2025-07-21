import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {
  form!: FormGroup;
  id!: string;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.form = this.fb.group({
      englishName: ['', Validators.required],
      arabicName: ['', Validators.required]
    });

    this.categoryService.getById(this.id).subscribe(category => {
      this.form.patchValue(category);
    });
  }

  submit() {
    if (this.form.valid) {
      this.categoryService.update(this.id, this.form.value).subscribe(() => {
        this.router.navigate(['/categories']);
      });
    }
  }
}
