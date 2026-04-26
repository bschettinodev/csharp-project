import { Box } from '@mui/material';
import { Outlet } from 'react-router-dom';

import { Header } from './Header';
import { Sidebar } from './Sidebar';

export function DashboardLayout() {
  return (
    <Box
      sx={{
        display: 'flex',
        minHeight: '100vh',
        bgcolor: '#07111f',
        color: 'common.white',
      }}
    >
      <Sidebar />

      <Box sx={{ flex: 1, minWidth: 0 }}>
        <Header />

        <Box
          component='main'
          sx={{
            px: { xs: 2, md: 4 },
            pt: {
              xs: 'calc(16px + var(--Header-height))',
              md: 3,
            },
            pb: { xs: 2, md: 4 },
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column',
            gap: 2,
          }}
        >
          <Outlet />
        </Box>
      </Box>
    </Box>
  );
}
