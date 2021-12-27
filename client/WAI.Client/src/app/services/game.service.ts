import {EventEmitter, Injectable} from '@angular/core';
import {HttpTransportType, HubConnection, HubConnectionBuilder, IHttpConnectionOptions} from "@aspnet/signalr";
import {environment} from "../../environments/environment";
import {from, Observable} from "rxjs";
import {GameData} from "../models/game-data";
import {GameMember} from "../models/game-member";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private hubConnection: HubConnection | undefined;

  gameConnectedEvent = new EventEmitter<GameData>();
  newMemberConnectedEvent = new EventEmitter<GameMember>();
  memberDisconnectedEvent = new EventEmitter<number>();

  constructor() {
  }

  public initConnection$(): Observable<any> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.apiUrl + 'game', this.options)
      .build();

    return from(this.hubConnection.start());
  }

  public connectToGame(gameId: number): void {
    this.hubConnection?.invoke('ConnectToGame', gameId);
  }

  public registerServerEvents(): void {
    this.hubConnection?.on('ConnectedToGame', (gameData: GameData) => this.gameConnectedEvent.next(gameData));
    this.hubConnection?.on("MemberConnected", (newGameMember: GameMember) => this.newMemberConnectedEvent.next(newGameMember));
    this.hubConnection?.on("MemberDisconnected", (disconnectedMemberId: number) => this.memberDisconnectedEvent.next(disconnectedMemberId));
  }

  public unregisterServerEvents(gameId: number): void {
    this.hubConnection?.invoke("DisconnectFromGame", gameId).then(_ => {
      this.hubConnection?.stop();
    })
  }

  private options: IHttpConnectionOptions = {
    accessTokenFactory: () => {
      return localStorage.getItem('jwt')!;
    },
    transport: HttpTransportType.LongPolling
  };
}
