import { Box, Stack, Typography } from '@mui/material';

import type { Transaction } from '@/api/transactions/transactions.types';
import { TransactionTypeApi } from '@/enums/transaction';

import { TransactionItem } from './TransactionItem';

type TransactionsSectionProps = {
  transactions: Transaction[];
};

export function TransactionsSection({
  transactions,
}: TransactionsSectionProps) {
  const todayTransactions = transactions;

  const total = todayTransactions.reduce((acc, transaction) => {
    if (transaction.type === TransactionTypeApi.Income) {
      return acc + transaction.amount;
    }

    return acc - transaction.amount;
  }, 0);

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

        <Typography sx={{ fontWeight: 900 }}>
          {total.toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL',
          })}
        </Typography>
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
          {new Date().toLocaleDateString('pt-BR')}
        </Typography>
      </Stack>

      <Stack sx={{ gap: 1.25 }}>
        {todayTransactions.map((transaction) => (
          <TransactionItem key={transaction.id} transaction={transaction} />
        ))}
      </Stack>
    </Box>
  );
}
