import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  form!: FormGroup;
  id!: string;

  constructor(private fb: FormBuilder, private userService: UserService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.form = this.fb.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['', Validators.required]
    });

    this.userService.getById(this.id).subscribe(user => {
      this.form.patchValue(user);
    });
  }

  submit() {
    if (this.form.valid) {
      this.userService.update(this.id, this.form.value).subscribe(() => {
        this.router.navigate(['/users']);
      });
    }
  }
}
