import {GameMember} from "./game-member";
import {GameStatus} from "./game-status";

export interface GameData {
  id: number;
  name: string;
  creator: GameMember;
  gameMembers: GameMember[];
  gameStatus: GameStatus;
}
