import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import {
  createBrowserRouter,
  Navigate,
  RouterProvider,
} from 'react-router-dom';

import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';

import { DashboardLayout } from '@/components/Layout/DashboardLayout/DashboardLayout';
import { DashboardPage } from '@/pages/Dashboard/DashboardPage';
import { LoginPage } from '@/pages/Login/LoginPage';

const theme = createTheme({
  palette: {
    mode: 'dark',
    background: {
      default: '#07111f',
    },
  },
});

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
    element: <DashboardLayout />,
    children: [
      {
        index: true,
        element: <DashboardPage />,
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
