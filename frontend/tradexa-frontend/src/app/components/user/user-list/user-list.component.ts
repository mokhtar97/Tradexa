import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.userService.getAll().subscribe(data => this.users = data);
  }

  deleteUser(id: string) {
    if (confirm('Are you sure?')) {
      this.userService.delete(id).subscribe(() => this.load());
    }
  }
}
