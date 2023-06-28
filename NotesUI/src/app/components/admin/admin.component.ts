import { Component, OnInit } from '@angular/core';
import { ServiceResponse } from 'src/app/models/serviceResponse';
import { User } from 'src/app/models/user';
import { AdminService } from 'src/app/services/admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  users: User[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    //this.getUsers();
  }

  getUsers(): void {
    this.adminService.getUsers().subscribe(
      (response: ServiceResponse<User[]>) => {this.users = response.data});
  }
}
