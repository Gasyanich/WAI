import {Component, OnDestroy, OnInit} from '@angular/core';
import {GamesServices} from "../../services/games-service.service";
import {Observable} from "rxjs";
import {GameInfo} from "../../game-info";


@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit, OnDestroy {

  games$: Observable<GameInfo[]>;

  constructor(private gameService: GamesServices) {
    this.gameService.initConnection();
    this.gameService.addGetGamesListener();
    this.games$ = this.gameService.games$;
  }

  ngOnInit(): void {
    this.gameService.createGame('testgamename');
  }

  ngOnDestroy(): void {
  }



}
