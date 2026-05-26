import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { theme } from '@/theme/theme';
import '@/theme/fonts';

import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from 'react-router-dom';

import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';

import { CssBaseline, ThemeProvider } from '@mui/material';
import { MobileAppLayout } from '@/components/Layout/MobileAppLayout/MobileAppLayout';
import { DashboardPage } from '@/pages/Dashboard/DashboardPage';

import { LoginPage } from '@/pages/Login/LoginPage';
import { AccountsPage } from '@/pages/Accounts/AccountsPage';
import { ProtectedRoute } from '@/router/ProtectedRoute';
import { AuthProvider } from '@/firebase/provider';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Navigate to='/login' />,
  },
  {
    path: '/login',
    element: <LoginPage />,
  },
  {
    path: '/dashboard',
    element: (
      <ProtectedRoute>
        <MobileAppLayout />
      </ProtectedRoute>
    ),
    children: [
      {
        index: true,
        element: <DashboardPage />,
      },
    ],
  },
  {
    path: '/accounts',
    element: (
      <ProtectedRoute>
        <MobileAppLayout />
      </ProtectedRoute>
    ),
    children: [
      {
        index: true,
        element: <AccountsPage />,
      },
    ],
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <CssBaseline />
        <AuthProvider>
          <RouterProvider router={router} />
        </AuthProvider>
      </LocalizationProvider>
    </ThemeProvider>
  </StrictMode>
);
