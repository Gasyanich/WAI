import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {

  constructor(private http: HttpClient) {
  }

  vkUrlRedirectLink = 'https://oauth.vk.com/authorize?' +
    'client_id=8028309&' +
    'display=popup&' +
    'redirect_uri=http://localhost:4200/callback&' +
    'scope=friends,offline,email,photos&' +
    'response_type=code&' +
    'v=5.131';


}
