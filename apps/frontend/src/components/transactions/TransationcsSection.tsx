import { Box, Stack, Typography } from '@mui/material';

import { TransactionItem } from './TransactionItem';

const transactions = [
  {
    title: 'Market',
    category: 'Food',
    amount: 'R$ 120,00',
    type: 'expense' as const,
  },
  {
    title: 'Salary',
    category: 'Income',
    amount: 'R$ 6.000,00',
    type: 'income' as const,
  },
  {
    title: 'Netflix',
    category: 'Subscription',
    amount: 'R$ 39,90',
    type: 'expense' as const,
  },
];

export function TransactionsSection() {
  return (
    <Box>
      <Stack
        direction='row'
        sx={{
          alignItems: 'center',
          justifyContent: 'space-between',
          mb: 0.5,
        }}
      >
        <Typography variant='h6' sx={{ fontWeight: 900 }}>
          Transactions
        </Typography>

        <Typography sx={{ fontWeight: 900 }}>- R$ 159,90</Typography>
      </Stack>

      <Stack
        direction='row'
        sx={{
          alignItems: 'center',
          gap: 1,
          mb: 1.5,
        }}
      >
        <Typography variant='body2' sx={{ color: 'text.secondary' }}>
          Today
        </Typography>

        <Typography variant='body2' sx={{ color: 'text.secondary' }}>
          27 Apr 2026
        </Typography>
      </Stack>

      <Stack sx={{ gap: 1.25 }}>
        {transactions.map((transaction) => (
          <TransactionItem key={transaction.title} {...transaction} />
        ))}
      </Stack>
    </Box>
  );
}
