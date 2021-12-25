import {Component, OnInit} from '@angular/core';
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
export class GameListComponent implements OnInit {

  games$: Observable<GameInfo[]> | undefined;

  constructor(private gamesService: GamesServices, private matDialog: MatDialog) {
  }

  ngOnInit(): void {
    this.loadGameList();
  }

  openCreateGameDialog(): void {
    const dialogRef = this.matDialog.open(CreateGameDialogComponent, {
      width: '100%',
      maxWidth: '600px'
    });

    dialogRef.afterClosed().subscribe((gameName: string) => {
      if (gameName && gameName.length > 0)
        this.gamesService.createGame$(gameName).subscribe();
    });
  }

  loadGameList(): void {
    this.games$ = this.gamesService.getGames$();
  }
}
