import { Box } from '@mui/material';
import { Outlet } from 'react-router-dom';

import { MobileBottomNav } from './MobileBottomNav';
import { MobileHeader } from './MobileHeader';

export function MobileAppLayout() {
  return (
    <Box
      sx={{
        minHeight: '100vh',
        bgcolor: 'background.default',
        color: 'text.primary',
      }}
    >
      <Box
        sx={{
          width: '100%',
          maxWidth: 430,
          minHeight: '100vh',
          mx: 'auto',
          position: 'relative',
          bgcolor: 'background.default',
          overflow: 'hidden',
        }}
      >
        <MobileHeader />

        <Box
          component='main'
          sx={{
            px: 2,
            pt: 2,
            pb: 10,
          }}
        >
          <Outlet />
        </Box>

        <MobileBottomNav />
      </Box>
    </Box>
  );
}
