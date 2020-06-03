import { AuthService } from './_services/auth.service';
import { Component, OnInit } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  constructor(private authSerivce: AuthService) {}
  ngOnInit(){
    const token = localStorage.getItem('token');
    if(token){
      this.authSerivce.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
