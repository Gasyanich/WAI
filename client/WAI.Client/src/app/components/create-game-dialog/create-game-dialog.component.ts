import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-create-game-dialog',
  templateUrl: './create-game-dialog.component.html',
  styleUrls: ['./create-game-dialog.component.scss']
})
export class CreateGameDialogComponent implements OnInit {

  gameName = '';

  constructor() {
  }

  ngOnInit(): void {
  }

}
