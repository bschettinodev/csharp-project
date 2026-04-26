import { useEffect, useState, type PropsWithChildren } from 'react';
import { onAuthStateChanged } from 'firebase/auth';

import { firebase } from './app';
import { AuthContext, type AuthContextValue } from './context';

export function AuthProvider({ children }: PropsWithChildren) {
  const [value, setValue] = useState<AuthContextValue>({
    status: 'loading',
    session: null,
  });

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(firebase().auth, async (user) => {
      if (!user) {
        setValue({
          status: 'unauthenticated',
          session: null,
        });

        return;
      }

      const idToken = await user.getIdToken();

      setValue({
        status: 'authenticated',
        session: {
          idToken,
          status: 'authenticated',
          profile: {
            email: user.email,
            displayName: user.displayName,
            emailVerified: user.emailVerified,
          },
        },
      });
    });

    return unsubscribe;
  }, []);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
