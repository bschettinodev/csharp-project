import { Box } from '@mui/material';

import { Header } from './Header';
import { Sidebar } from './Sidebar';

type DashboardLayoutProps = {
  children: React.ReactNode;
};

export function DashboardLayout({ children }: DashboardLayoutProps) {
  return (
    <Box
      sx={{
        display: 'flex',
        minHeight: '100vh',
        bgcolor: '#07111f',
        color: 'common.white',
        background:
          'radial-gradient(circle at top right, rgba(34, 197, 94, 0.12), transparent 32%), #07111f',
      }}
    >
      <Sidebar />

      <Box
        sx={{
          flex: 1,
          minWidth: 0,
          display: 'flex',
          flexDirection: 'column',
        }}
      >
        <Header />

        <Box
          component='main'
          sx={{
            flex: 1,
            width: '100%',
            maxWidth: 1440,
            mx: 'auto',
            px: { xs: 2, sm: 3, md: 4 },
            pt: {
              xs: 'calc(16px + var(--Header-height))',
              md: 3,
            },
            pb: { xs: 2, md: 4 },
          }}
        >
          {children}
        </Box>
      </Box>
    </Box>
  );
}
