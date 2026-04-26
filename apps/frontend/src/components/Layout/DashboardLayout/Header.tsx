import {
  Avatar,
  Box,
  IconButton,
  InputBase,
  Stack,
  Typography,
} from '@mui/material';
import NotificationsRoundedIcon from '@mui/icons-material/NotificationsRounded';
import SearchIcon from '@mui/icons-material/Search';

export function Header() {
  return (
    <Box
      component='header'
      sx={{
        width: '100%',
        px: { xs: 2, md: 4 },
        pt: { xs: 8, md: 2 },
        pb: 2,
      }}
    >
      <Stack
        direction='row'
        sx={{ alignItems: 'center', justifyContent: 'space-between' }}
      >
        <Box>
          <Typography variant='body2' sx={{ color: 'rgba(255,255,255,0.56)' }}>
            Welcome back
          </Typography>

          <Typography variant='h5' sx={{ fontWeight: 800 }}>
            Gabriel
          </Typography>
        </Box>

        <Stack direction='row' sx={{ alignItems: 'center', gap: 2 }}>
          <Box
            sx={{
              display: { xs: 'none', md: 'flex' },
              alignItems: 'center',
              px: 2,
              py: 0.5,
              borderRadius: 3,
              bgcolor: 'rgba(255,255,255,0.06)',
            }}
          >
            <SearchIcon sx={{ fontSize: 18, mr: 1 }} />
            <InputBase
              placeholder='Search...'
              sx={{
                color: 'white',
                fontSize: 14,
              }}
            />
          </Box>

          <IconButton
            sx={{
              color: 'white',
              border: '1px solid rgba(255,255,255,0.16)',
            }}
          >
            <NotificationsRoundedIcon />
          </IconButton>

          <Avatar sx={{ width: 36, height: 36 }}>G</Avatar>
        </Stack>
      </Stack>
    </Box>
  );
}
