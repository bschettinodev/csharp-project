import {
  AccountBalanceWallet,
  BarChartRounded,
  CategoryRounded,
  DashboardRounded,
} from '@mui/icons-material';
import {
  Avatar,
  Box,
  Divider,
  Drawer,
  List,
  ListItemButton,
  ListItemIcon,
  Stack,
  Typography,
} from '@mui/material';

const drawerWidth = 260;

const routes = [
  { label: 'Dashboard', icon: <DashboardRounded /> },
  { label: 'Accounts', icon: <AccountBalanceWallet /> },
  { label: 'Transactions', icon: <BarChartRounded /> },
  { label: 'Categories', icon: <CategoryRounded /> },
];

export function Sidebar() {
  return (
    <Drawer
      variant='permanent'
      sx={{
        display: { xs: 'none', md: 'block' },
        width: drawerWidth,
        flexShrink: 0,
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          boxSizing: 'border-box',
          bgcolor: '#0b1628',
          color: 'common.white',
          borderRight: '1px solid rgba(255,255,255,0.08)',
        },
      }}
    >
      <Box sx={{ p: 3 }}>
        <Typography variant='h5' sx={{ fontWeight: 800 }}>
          Finance
        </Typography>

        <Typography variant='body2' sx={{ color: 'rgba(255,255,255,0.56)' }}>
          Personal tracker
        </Typography>
      </Box>

      <Divider sx={{ borderColor: 'rgba(255,255,255,0.08)' }} />

      <Box
        sx={{
          flex: 1,
          overflow: 'auto',
          px: 2,
          py: 2,
        }}
      >
        <List sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          {routes.map((item) => (
            <ListItemButton
              key={item.label}
              sx={{
                borderRadius: 3,
                color: 'rgba(255,255,255,0.72)',
                '&:hover': {
                  bgcolor: 'rgba(255,255,255,0.08)',
                  color: 'common.white',
                },
              }}
            >
              <ListItemIcon sx={{ color: 'inherit', minWidth: 40 }}>
                {item.icon}
              </ListItemIcon>

              <Typography variant='body2' sx={{ fontWeight: 600 }}>
                {item.label}
              </Typography>
            </ListItemButton>
          ))}
        </List>
      </Box>

      <Divider sx={{ borderColor: 'rgba(255,255,255,0.08)' }} />

      <Stack
        direction='row'
        sx={{
          p: 2,
          gap: 1.5,
          alignItems: 'center',
        }}
      >
        <Avatar sx={{ width: 36, height: 36 }}>G</Avatar>

        <Box sx={{ minWidth: 0 }}>
          <Typography variant='body2' sx={{ fontWeight: 700 }}>
            Gabriel
          </Typography>

          <Typography
            variant='caption'
            sx={{
              color: 'rgba(255,255,255,0.56)',
              display: 'block',
            }}
          >
            Personal account
          </Typography>
        </Box>
      </Stack>
    </Drawer>
  );
}
