import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GameListComponent} from "./components/game-list/game-list.component";
import {LoginPageComponent} from "./components/login-page/login-page.component";
import {VkCallbackComponent} from "./components/vk-callback/vk-callback.component";
import {AuthGuard} from "./guards/auth.guard";
import {GamePageComponent} from "./components/game-page/game-page.component";

const routes: Routes = [
  {path: '', redirectTo: 'games', pathMatch: 'full'},
  {path: 'games', component: GameListComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginPageComponent},
  {path: 'vkcallback', component: VkCallbackComponent},
  {path: 'games/:id', component: GamePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
