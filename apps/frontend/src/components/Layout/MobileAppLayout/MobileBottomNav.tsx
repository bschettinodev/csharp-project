import { useNavigate } from 'react-router-dom';

import AddRoundedIcon from '@mui/icons-material/AddRounded';
import BarChartRoundedIcon from '@mui/icons-material/BarChartRounded';
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import ReceiptLongRoundedIcon from '@mui/icons-material/ReceiptLongRounded';
import WalletRoundedIcon from '@mui/icons-material/AccountBalanceWalletRounded';
import { Box, Fab, IconButton, Paper } from '@mui/material';

export function MobileBottomNav() {
  const navigate = useNavigate();

  return (
    <Paper
      elevation={0}
      sx={{
        position: 'fixed',
        left: '50%',
        bottom: 16,
        transform: 'translateX(-50%)',
        width: 'calc(100% - 32px)',
        maxWidth: 398,
        height: 68,
        borderRadius: 999,
        bgcolor: 'background.paper',
        border: '1px solid rgba(255,255,255,0.08)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-around',
        px: 1.5,
        zIndex: 10,
      }}
    >
      <IconButton color='primary' onClick={() => navigate('/dashboard')}>
        <HomeRoundedIcon />
      </IconButton>

      <IconButton sx={{ color: 'text.secondary' }}>
        <BarChartRoundedIcon />
      </IconButton>

      <Box sx={{ width: 56 }} />

      <IconButton sx={{ color: 'text.secondary' }}>
        <ReceiptLongRoundedIcon />
      </IconButton>

      <IconButton
        sx={{ color: 'text.secondary' }}
        onClick={() => navigate('/accounts')}
      >
        <WalletRoundedIcon />
      </IconButton>

      <Fab
        color='primary'
        size='medium'
        sx={{
          position: 'absolute',
          top: -24,
          left: '50%',
          transform: 'translateX(-50%)',
          boxShadow: '0 12px 28px rgba(248,255,79,0.28)',
        }}
      >
        <AddRoundedIcon />
      </Fab>
    </Paper>
  );
}
