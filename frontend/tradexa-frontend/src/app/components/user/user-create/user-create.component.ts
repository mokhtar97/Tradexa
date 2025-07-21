import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  submit() {
    if (this.form.valid) {
      this.userService.create(this.form.value).subscribe(() => {
        this.router.navigate(['/users']);
      });
    }
  }
}
