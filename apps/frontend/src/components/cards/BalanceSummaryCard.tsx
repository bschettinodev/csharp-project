import ArrowDownwardRoundedIcon from '@mui/icons-material/ArrowDownwardRounded';
import ArrowUpwardRoundedIcon from '@mui/icons-material/ArrowUpwardRounded';
import { Box, Card, Stack, Typography } from '@mui/material';

type BalanceSummaryCardProps = {
  totalBalance: number;
  totalIncome: number;
  totalExpenses: number;
};

function formatCurrency(value: number) {
  return value.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  });
}

export function BalanceSummaryCard({
  totalBalance,
  totalIncome,
  totalExpenses,
}: BalanceSummaryCardProps) {
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
          fontSize: 'clamp(30px, 9vw, 40px)',
          fontWeight: 900,
          mt: 0.5,
          whiteSpace: 'nowrap',
        }}
      >
        {formatCurrency(totalBalance)}
      </Typography>

      <Stack
        direction='row'
        sx={{
          mt: 2.5,
          justifyContent: 'center',
          gap: 2.5,
        }}
      >
        <Stack sx={{ alignItems: 'center', minWidth: 120 }} spacing={0.3}>
          <Stack direction='row' sx={{ alignItems: 'center' }} spacing={0.8}>
            <Box
              sx={{
                width: 30,
                height: 30,
                borderRadius: '50%',
                bgcolor: '#55E27A',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                flexShrink: 0,
              }}
            >
              <ArrowDownwardRoundedIcon
                sx={{ fontSize: 18, color: '#000' }} //
              />
            </Box>

            <Typography
              sx={{
                fontSize: 18,
                fontWeight: 900,
                whiteSpace: 'nowrap',
              }}
            >
              {formatCurrency(totalIncome)}
            </Typography>
          </Stack>

          <Typography
            variant='caption'
            sx={{ fontWeight: 700, opacity: 0.7, mt: -0.2 }}
          >
            Total Income
          </Typography>
        </Stack>

        <Stack sx={{ alignItems: 'center', minWidth: 120 }} spacing={0.3}>
          <Stack direction='row' sx={{ alignItems: 'center' }} spacing={0.8}>
            <Box
              sx={{
                width: 30,
                height: 30,
                borderRadius: '50%',
                bgcolor: '#FF5252',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                flexShrink: 0,
              }}
            >
              <ArrowUpwardRoundedIcon sx={{ fontSize: 18, color: '#000' }} />
            </Box>

            <Typography
              sx={{
                fontSize: 18,
                fontWeight: 900,
                whiteSpace: 'nowrap',
              }}
            >
              {formatCurrency(totalExpenses)}
            </Typography>
          </Stack>

          <Typography
            variant='caption'
            sx={{ fontWeight: 700, opacity: 0.7, mt: -0.2 }}
          >
            Total Expenses
          </Typography>
        </Stack>
      </Stack>
    </Card>
  );
}
