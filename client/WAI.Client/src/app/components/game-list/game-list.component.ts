import {Component, OnDestroy, OnInit} from '@angular/core';
import {GamesServices} from "../../services/games-service.service";
import {Observable} from "rxjs";
import {GameInfo} from "../../game-info";
import {MatDialog} from "@angular/material/dialog";
import {CreateGameDialogComponent} from "../create-game-dialog/create-game-dialog.component";


@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit, OnDestroy {

  games$: Observable<GameInfo[]>;

  constructor(private gameService: GamesServices, private matDialog: MatDialog) {
    this.gameService.initConnection();
    this.gameService.addGetGamesListener();
    this.games$ = this.gameService.games$;
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
  }

  public openCreateGameDialog(): void {
    const dialogRef = this.matDialog.open(CreateGameDialogComponent, {
      width: '100%',
      maxWidth: '600px'
    });

    dialogRef.afterClosed().subscribe((gameName: string) => {
      console.log(gameName);
      if (gameName && gameName.length > 0)
        this.gameService.createGame(gameName).subscribe();
    });
  }


}
