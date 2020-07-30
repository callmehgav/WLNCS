import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {WebSocketService} from './web-socket.service';

import { AppComponent } from './app.component';
import { CesiumDirective } from './cesium.directive';


@NgModule({
  declarations: [
    AppComponent,
    CesiumDirective
  ],
  imports: [
    BrowserModule,
  ],
  providers: [
    WebSocketService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
