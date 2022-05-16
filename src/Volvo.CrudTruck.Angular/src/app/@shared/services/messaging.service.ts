import { Injectable } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/compat/messaging';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { getMessaging, getToken } from "firebase/messaging";
import { SwPush } from '@angular/service-worker';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {
  currentMessage = new BehaviorSubject(null);
  constructor(private angularFireMessaging: AngularFireMessaging, private swPush: SwPush) {
  this.angularFireMessaging.messages.subscribe(
  (message) => {
    // message.onMessage = message.onMessage.bind(message);
    // message.onTokenRefresh = message.onTokenRefresh.bind(message);
    console.log(message)
    })
  }
  requestPermission() {
    this.swPush.requestSubscription({
      serverPublicKey: environment.vapidPublicKey
  })
  .then(sub => console.log(sub))
  .catch(err => console.error("Could not subscribe to notifications", err));
  }
  receiveMessage() {
    this.angularFireMessaging.messages.subscribe(
      (payload) => {
      console.log("new message received. ", payload);
      this.currentMessage.next(payload);
    })
  }
}
