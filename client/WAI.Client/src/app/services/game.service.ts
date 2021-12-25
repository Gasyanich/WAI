import {Injectable} from '@angular/core';
import {HttpTransportType, HubConnection, HubConnectionBuilder, IHttpConnectionOptions} from "@aspnet/signalr";
import {environment} from "../../environments/environment";
import {from, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private hubConnection: HubConnection | undefined;

  constructor() {
  }

  public initConnection(): Observable<any> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.apiUrl + 'game', this.options)
      .build();

    return from(this.hubConnection.start());
  }

  public connectToGame(gameId: number): void {
    this.hubConnection?.invoke('ConnectToGame', gameId)
      .then(result => {
        console.log(result);
      });
  }

  private options: IHttpConnectionOptions = {
    accessTokenFactory: () => {
      return localStorage.getItem('jwt')!;
    },
    transport: HttpTransportType.LongPolling
  };
}
