import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {GameInfo} from "../game-info";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class GamesServices {

  constructor(private http: HttpClient) {
  }

  public createGame$(gameName: string): Observable<any> {
    return this.http.post('games', {gameName});
  }

  public getGames$(): Observable<GameInfo[]> {
    return this.http.get<GameInfo[]>('games');
  }
}
