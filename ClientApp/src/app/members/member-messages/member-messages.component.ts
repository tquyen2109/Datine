import { AlertifyService } from './../../_services/alertify.service';
import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/_models/message';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input() recipientId: number;
  messages: Message[];
  constructor(private userService: UserService, private authService: AuthService,
              private alertify: AlertifyService) { }
  newMessage: any = {};
  ngOnInit() {
    this.loadMessages();
  }
  loadMessages() {
    this.userService.getMessageThread(this.authService.decodedToken.nameid, this.recipientId).subscribe(messages => {
      this.messages = messages;
    }, error => {
      this.alertify.error(error);
    });
  }
  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid, this.newMessage).subscribe((message: Message) => {
      this.messages.unshift(message);
      this.newMessage = '';
    }, error => {
      this.alertify.error(error);
    });
  }
}
