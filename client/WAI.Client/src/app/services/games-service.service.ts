import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";
import {environment} from "../../environments/environment";
import {Observable, Subject} from "rxjs";
import {GameInfo} from "../game-info";

@Injectable({
  providedIn: 'root'
})
export class GamesServices {

  private hubConnection: HubConnection | undefined;
  private gamesSubject: Subject<GameInfo[]> = new Subject<GameInfo[]>();

  get games$(): Observable<GameInfo[]> {
    return this.gamesSubject.asObservable();
  }

  constructor() {
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
    this.hubConnection?.invoke('CreateGame', gameName);
  }

  public addGetGamesListener(): void {
    this.hubConnection?.on("GetGames", (data: GameInfo[]) => {
      this.gamesSubject.next(data);
    });
  }
}
