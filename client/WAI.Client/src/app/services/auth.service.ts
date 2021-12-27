import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public vkUrlRedirectLink = 'https://oauth.vk.com/authorize?' +
    'client_id=8028309&' +
    'display=popup&' +
    'redirect_uri=http://localhost:4200/vkcallback&' +
    'scope=friends,offline,email,photos&' +
    'response_type=code&' +
    'v=5.131';

  constructor(private http: HttpClient, private router: Router) {
  }

  public loginVk(code: string): void {
    this.http.post<TokenResponse>('auth', {code})
      .subscribe((response: TokenResponse) => {
        localStorage.setItem('isAuth', 'true');
        localStorage.setItem('jwt', response.token);
        localStorage.setItem('userId', response.userId.toString());
        this.router.navigateByUrl('/games');
      });
  }

  public redirectToVkLogin(): void {
    this.http.get('auth').subscribe(val => console.log(val));
  }

  public isAuthenticated(): boolean {
    return localStorage.getItem('isAuth') === 'true';
  }
}

export interface TokenResponse {
  token: string;
  userId: number;
}
