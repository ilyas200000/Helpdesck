import {Component, OnInit} from '@angular/core';
import { UserService } from '../authentification/_services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit{
  adminPubContent!: string;
  constructor(private userService : UserService) {
  }
  ngOnInit(): void {
    this.userService.getUserPublicContent().subscribe({
      next : data => {
        this.adminPubContent = data;
      },
      error : err => console.log(err)
    })
  }

}
