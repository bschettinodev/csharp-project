import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { theme } from '@/theme/theme';
import '@/theme/fonts';

import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from 'react-router-dom';

import { CssBaseline, ThemeProvider } from '@mui/material';
import { MobileAppLayout } from '@/components/Layout/MobileAppLayout/MobileAppLayout';
import { DashboardPage } from '@/pages/Dashboard/DashboardPage';

import { LoginPage } from '@/pages/Login/LoginPage';
import { AccountsPage } from '@/pages/Accounts/AccountsPage';

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
    element: <MobileAppLayout />,
    children: [
      {
        index: true,
        element: <DashboardPage />,
      },
    ],
  },
  {
    path: '/accounts',
    element: <MobileAppLayout />,
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
      <CssBaseline />
      <RouterProvider router={router} />
    </ThemeProvider>
  </StrictMode>
);
