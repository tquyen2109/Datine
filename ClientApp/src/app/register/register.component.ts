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
  constructor() { }

  ngOnInit() {
  }
  register(){
    console.log(this.model);

  }
  cancel() {
    this.cancellRegister.emit(false);
  }
}
