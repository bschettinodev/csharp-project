import { useContext } from 'react';

import { AuthContext } from './context';

export function useSession() {
  return useContext(AuthContext);
}
