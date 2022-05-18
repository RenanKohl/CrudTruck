import { Injectable } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/compat/messaging';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { getMessaging, getToken } from 'firebase/messaging';
import { SwPush } from '@angular/service-worker';
import { environment } from '@env/environment';
import { mergeMap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessagingService {
  currentMessage = new BehaviorSubject(null);
  constructor(/*private angularFireMessaging: AngularFireMessaging, */ private swPush: SwPush) {}
  requestPermission() {
    // this.angularFireMessaging.requestToken.subscribe({
    //   next: token => {
    //     console.log(token)
    //     // Upload the user FCM token to your server.
    //   },
    //   error: err => {
    //     console.error('Fetching FCM token failed: ', +err)
    //   }
    // })
    this.swPush
      .requestSubscription({
        serverPublicKey: environment.vapidPublicKey,
      })
      .then((sub) => {
        console.log('Notification Subscription: ', sub);
      })
      .catch((err) => console.error('Could not subscribe to notifications', err));
  }
  receiveMessage() {
    this.swPush.messages.subscribe((payload) => {
      console.log('new message received. ', payload);
      this.currentMessage.next(payload);
    });
  }

  getToken() {
    // this.angularFireMessaging.getToken
    // .subscribe(
    //     (token) => { console.log('Token');console.log(token); },
    //   );
  }
}
