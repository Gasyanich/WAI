import {Injectable} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {GameInfo} from "../models/game-info";

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  constructor(private http: HttpClient) {
  }

  public createGame$(gameName: string): Observable<any> {
    return this.http.post('games', {gameName});
  }

  public getGames$(): Observable<GameInfo[]> {
    return this.http.get<GameInfo[]>('games');
  }
}
