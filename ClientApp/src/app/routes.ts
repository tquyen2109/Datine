import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { HomeComponent } from './home/home.component';
import {Routes} from '@angular/router';
import { MemberListComponent } from './member-list/member-list.component';
export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: 'members', component: MemberListComponent},
    {path: 'messages', component: MessagesComponent},
    {path: 'list', component: ListsComponent},
    {path: '**', redirectTo: 'home', pathMatch: 'full'}
];

