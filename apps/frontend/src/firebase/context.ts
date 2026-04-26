import { createContext } from 'react';

export type SessionStatus = 'loading' | 'authenticated' | 'unauthenticated';

export type UserProfile = {
  email: string | null;
  displayName: string | null;
  emailVerified: boolean;
};

export type Session = {
  idToken: string;
  status: 'authenticated';
  profile: UserProfile;
};

export type AuthContextValue = {
  status: SessionStatus;
  session: Session | null;
};

export const AuthContext = createContext<AuthContextValue>({
  status: 'loading',
  session: null,
});
