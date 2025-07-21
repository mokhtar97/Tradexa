import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
})
export class ProductEditComponent implements OnInit {
  form: FormGroup;
  id!: string;
 categories:any[] = [];
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
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

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    // this.productService.getProduct(this.id).subscribe(product => {
    //   this.form.patchValue(product);
    // });
  }

  submit() {
    if (this.form.valid) {
      // this.productService.updateProduct(this.id, this.form.value).subscribe(() => {
      //   this.router.navigate(['/products']);
      // });
    }
  }
}
