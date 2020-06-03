import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Input, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancellRegister = new EventEmitter();
  constructor(private authService: AuthService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }
  register(){
   this.authService.register(this.model).subscribe(() => {
     this.alertify.sucess('registration successful')
   }, error => {
     this.alertify.error(error);
   });

  }
  cancel() {
    this.cancellRegister.emit(false);
  }
}
