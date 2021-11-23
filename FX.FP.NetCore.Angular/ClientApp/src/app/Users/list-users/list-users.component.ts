import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { User } from '../user';


@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit {
  public displayColumns: string[] = ['id', 'createdOn', 'userName', 'email'];
  public users: User[];


  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.list().subscribe(result => {
      this.users = result.data;
      console.log(this.users);
    }, error => console.error(error));
  }

}
