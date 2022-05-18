import { initializeApp } from "firebase/app";
import { getMessaging } from "firebase/messaging/sw";

const firebaseConfig = {
  apiKey: "AIzaSyBIQH-QtQoL9pMJMH29pn8fRCVJwf7m6I0",
  authDomain: "crudtruck.firebaseapp.com",
  projectId: "crudtruck",
  storageBucket: "crudtruck.appspot.com",
  messagingSenderId: "203765445359",
  appId: "1:203765445359:web:6b9e12339192fa61e9e11a"
};

const app = initializeApp(firebaseConfig, 'crudtruck');

const messaging = getMessaging(app);