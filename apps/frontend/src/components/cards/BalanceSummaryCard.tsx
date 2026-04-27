import ArrowDownwardRoundedIcon from '@mui/icons-material/ArrowDownwardRounded';
import ArrowUpwardRoundedIcon from '@mui/icons-material/ArrowUpwardRounded';
import { Box, Card, Stack, Typography } from '@mui/material';

export function BalanceSummaryCard() {
  return (
    <Card
      sx={{
        p: 2.5,
        borderRadius: 6,
        bgcolor: 'primary.main',
        color: 'primary.contrastText',
        textAlign: 'center',
      }}
    >
      <Typography variant='body2' sx={{ fontWeight: 700, opacity: 0.7 }}>
        Total Balance
      </Typography>

      <Typography
        sx={{
          fontSize: 40,
          fontWeight: 900,
          mt: 0.5,
        }}
      >
        R$ 4.250
      </Typography>

      <Stack
        direction='row'
        sx={{
          mt: 3,
          justifyContent: 'center',
          gap: 4,
        }}
      >
        <Stack sx={{ alignItems: 'center' }} spacing={0.3}>
          <Stack direction='row' sx={{ alignItems: 'center' }} spacing={1}>
            <Box
              sx={{
                width: 34,
                height: 34,
                borderRadius: '50%',
                bgcolor: '#55E27A',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
              }}
            >
              <ArrowDownwardRoundedIcon sx={{ fontSize: 20, color: '#000' }} />
            </Box>

            <Typography
              sx={{
                fontSize: 22,
                fontWeight: 900,
              }}
            >
              R$ 6.000
            </Typography>
          </Stack>

          <Typography
            variant='caption'
            sx={{
              fontWeight: 700,
              opacity: 0.7,
              mt: -0.2,
            }}
          >
            Total Income
          </Typography>
        </Stack>

        <Stack sx={{ alignItems: 'center' }} spacing={0.3}>
          <Stack direction='row' sx={{ alignItems: 'center' }} spacing={1}>
            <Box
              sx={{
                width: 34,
                height: 34,
                borderRadius: '50%',
                bgcolor: '#FF5252',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
              }}
            >
              <ArrowUpwardRoundedIcon sx={{ fontSize: 20, color: '#000' }} />
            </Box>

            <Typography
              sx={{
                fontSize: 22,
                fontWeight: 900,
              }}
            >
              R$ 1.750
            </Typography>
          </Stack>

          <Typography
            variant='caption'
            sx={{
              fontWeight: 700,
              opacity: 0.7,
              mt: -0.2,
            }}
          >
            Total Expenses
          </Typography>
        </Stack>
      </Stack>
    </Card>
  );
}
