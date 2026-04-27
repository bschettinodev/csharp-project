import { createTheme } from '@mui/material/styles';

import { palette } from '@/theme/pallete';

export const theme = createTheme({
  palette: {
    mode: 'dark',
    ...palette,
  },
  shape: {
    borderRadius: 20,
  },
  typography: {
    fontFamily: 'Inter, sans-serif',
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 999,
          textTransform: 'none',
          fontWeight: 700,
        },
      },
    },
  },
});
