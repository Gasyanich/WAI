import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from "@angular/material/button";
import {MatTabsModule} from "@angular/material/tabs";
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import { GameListComponent } from './components/game-list/game-list.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import {HttpClientModule} from "@angular/common/http";
import { VkCallbackComponent } from './components/vk-callback/vk-callback.component';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatListModule} from "@angular/material/list";

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    LoginPageComponent,
    VkCallbackComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatTabsModule,
        MatIconModule,
        MatToolbarModule,
        HttpClientModule,
        MatProgressSpinnerModule,
        MatListModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
