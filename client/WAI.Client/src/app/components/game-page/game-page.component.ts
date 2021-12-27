import {Component, OnDestroy, OnInit} from '@angular/core';
import {GameService} from "../../services/game.service";
import {ActivatedRoute} from "@angular/router";
import {GameData} from "../../models/game-data";

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.scss']
})
export class GamePageComponent implements OnInit, OnDestroy {

  gameId: number | undefined;
  gameData: GameData | undefined;
  userId: number | undefined;

  constructor(private gameService: GameService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.gameId = +params['id'];
    });

    const userIdStr = localStorage.getItem('userId');
    if (userIdStr)
      this.userId = +userIdStr;
  }

  isCreator(): boolean {
    return this.gameData?.creator.userId === this.userId;
  }

  ngOnInit(): void {
    this.gameService.initConnection$().subscribe(() => {
      this.gameService.registerServerEvents();

      this.gameService.gameConnectedEvent.subscribe(gameData => this.gameData = gameData);
      this.gameService.newMemberConnectedEvent.subscribe(newGameMember => this.gameData?.gameMembers.push(newGameMember));

      this.gameService.memberDisconnectedEvent.subscribe(disconnectedMemberId => {
        this.gameData!.gameMembers = this.gameData!.gameMembers.filter(gm => gm.userId != disconnectedMemberId);
      });

      this.gameService.connectToGame(this.gameId!);
    });
  }

  ngOnDestroy(): void {
    this.gameService.unregisterServerEvents(this.gameId!);
  }


}
