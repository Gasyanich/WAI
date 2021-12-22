import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {

  public vkUrlRedirectLink: string;

  constructor(private auth: AuthService) {
    this.vkUrlRedirectLink = auth.vkUrlRedirectLink;
  }

  public redirectToVkLoginPage(): void{
    this.auth.redirectToVkLogin();
  }
}
