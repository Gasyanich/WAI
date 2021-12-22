import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GameListComponent} from "./components/game-list/game-list.component";
import {LoginPageComponent} from "./components/login-page/login-page.component";
import {VkCallbackComponent} from "./components/vk-callback/vk-callback.component";
import {AuthGuard} from "./guards/auth.guard";

const routes: Routes = [
  {path: '', redirectTo: 'games', pathMatch: 'full'},
  {path: 'games', component: GameListComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginPageComponent},
  {path: 'vkcallback', component: VkCallbackComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
