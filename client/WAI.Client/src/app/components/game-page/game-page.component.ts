import {Component, OnInit} from '@angular/core';
import {GameService} from "../../services/game.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.scss']
})
export class GamePageComponent implements OnInit {

  gameId: number | undefined;

  constructor(private gameService: GameService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.gameId = +params['id'];
    });
    
    this.gameService.initConnection().subscribe(() => {
      if (this.gameId)
        this.gameService.connectToGame(this.gameId);
    });

  }

}
