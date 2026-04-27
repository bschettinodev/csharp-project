import { Box, Card, Stack, Typography } from '@mui/material';

type TransactionItemProps = {
  title: string;
  category: string;
  amount: string;
  type: 'income' | 'expense';
};

export function TransactionItem({
  title,
  category,
  amount,
  type,
}: TransactionItemProps) {
  const isIncome = type === 'income';

  return (
    <Card
      sx={{
        p: 1.5,
        borderRadius: 4,
        bgcolor: 'background.paper',
        backgroundImage: 'none',
        boxShadow: 'none',
      }}
    >
      <Stack
        direction='row'
        sx={{
          alignItems: 'center',
          justifyContent: 'space-between',
        }}
      >
        <Stack direction='row' sx={{ alignItems: 'center', gap: 1.5 }}>
          <Box
            sx={{
              width: 42,
              height: 42,
              borderRadius: '50%',
              bgcolor: isIncome ? '#55E27A' : '#FF7A7A',
            }}
          />

          <Box>
            <Typography sx={{ fontWeight: 800 }}>{title}</Typography>
            <Typography variant='caption' color='text.secondary'>
              {category}
            </Typography>
          </Box>
        </Stack>

        <Typography
          sx={{
            fontWeight: 900,
            color: isIncome ? '#55E27A' : '#FF7A7A',
          }}
        >
          {isIncome ? '+' : '-'} {amount}
        </Typography>
      </Stack>
    </Card>
  );
}
