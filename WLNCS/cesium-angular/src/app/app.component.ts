import { Component } from '@angular/core';
import { WebSocketService } from './web-socket.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  announcementSub;
  messages: string[] = [];
  name: string = "";
  constructor(private socketService: WebSocketService) {
    this.socketService.announcement$.subscribe(announcement => {
      if (announcement) {
        this.messages.unshift(announcement);
      }
    });
    this.socketService.name$.subscribe(n => {
      this.name = n;
    });
  }

  ngOnInit() {
    this.socketService.startSocket();

  }
}