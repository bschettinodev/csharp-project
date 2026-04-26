import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';

import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';

import App from './App';

const theme = createTheme({
  palette: {
    mode: 'dark',
    background: {
      default: '#07111f',
    },
  },
});

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <App />
    </ThemeProvider>
  </StrictMode>
);
