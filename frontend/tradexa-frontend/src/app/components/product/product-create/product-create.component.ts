import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
})
export class ProductCreateComponent {
  form: FormGroup;
 categories:any[] = [];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router
  ) {
    this.form = this.fb.group({
      englishName: ['', Validators.required],
      arabicName: ['', Validators.required],
      categoryId: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      stock: [0, [Validators.required, Validators.min(0)]],
    });
  }

  submit() {
    if (this.form.valid) {
      // this.productService.createProduct(this.form.value).subscribe(() => {
      //   this.router.navigate(['/products']);
      // });
    }
  }
}
