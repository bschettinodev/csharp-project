import { Navigate } from 'react-router-dom';

import { useSession } from '@/firebase/hooks';

type ProtectedRouteProps = {
  children: React.ReactNode;
};

export function ProtectedRoute({ children }: ProtectedRouteProps) {
  const { status } = useSession();

  if (status === 'loading') {
    return null;
  }

  if (status === 'unauthenticated') {
    return <Navigate to='/login' replace />;
  }

  return children;
}
