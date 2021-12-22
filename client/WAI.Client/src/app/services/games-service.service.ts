import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import {environment} from "../../environments/environment";
import {Observable, Subject} from "rxjs";
import {GameInfo} from "../game-info";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class GamesServices {

  private hubConnection: HubConnection | undefined;
  private gamesSubject: Subject<GameInfo[]> = new Subject<GameInfo[]>();

  get games$(): Observable<GameInfo[]> {
    return this.gamesSubject.asObservable();
  }

  constructor(private http: HttpClient) {
  }

  public initConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.apiUrl + 'games')
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error'));
  }

  public createGame(gameName: string): void {
    this.http.post('games', {gameName}).subscribe();
  }

  public addGetGamesListener(): void {
    this.hubConnection?.on("GetGames", (data: GameInfo[]) => {
      this.gamesSubject.next(data);
    });
  }
}
