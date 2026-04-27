import NotificationsRoundedIcon from '@mui/icons-material/NotificationsRounded';
import { Avatar, Box, IconButton, Typography } from '@mui/material';

export function MobileHeader() {
  return (
    <Box
      component='header'
      sx={{
        px: 2,
        pt: 3,
        pb: 1,
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
      }}
    >
      <Box>
        <Typography variant='body2' color='text.secondary'>
          Welcome back
        </Typography>

        <Typography variant='h6' sx={{ fontWeight: 800 }}>
          Gabriel
        </Typography>
      </Box>

      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
        <IconButton
          sx={{
            color: 'text.primary',
            bgcolor: 'background.paper',
          }}
        >
          <NotificationsRoundedIcon />
        </IconButton>

        <Avatar sx={{ width: 38, height: 38 }}>G</Avatar>
      </Box>
    </Box>
  );
}
