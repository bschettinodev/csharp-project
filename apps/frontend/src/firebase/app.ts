import { initializeApp, getApps, type FirebaseApp } from 'firebase/app';

import {
  browserLocalPersistence,
  getAuth,
  setPersistence,
  signInWithEmailAndPassword,
  signOut,
  type Auth,
} from 'firebase/auth';

const authHandlers = (auth: Auth) => {
  return {
    signIn: (options: { email: string; password: string }) => {
      return signInWithEmailAndPassword(auth, options.email, options.password);
    },

    signOut: () => {
      return signOut(auth);
    },
  };
};

const createFirebaseClient = (app: FirebaseApp) => {
  const auth = getAuth(app);

  setPersistence(auth, browserLocalPersistence);

  return {
    app,
    auth,
    handlers: authHandlers(auth),
  };
};

export function firebase() {
  let [app] = getApps();

  if (!app) {
    app = initializeApp({
      apiKey: import.meta.env.VITE_FIREBASE_API_KEY,
      authDomain: import.meta.env.VITE_FIREBASE_AUTH_DOMAIN,
    });
  }

  return createFirebaseClient(app);
}
