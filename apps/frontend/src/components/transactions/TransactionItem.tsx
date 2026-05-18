import { Box, Card, Stack, Typography } from '@mui/material';

import type { Transaction } from '@/api/transactions/transactions.types';
import { CategoryIcon } from '@/components/common/CategoryIcon';
import { TransactionTypeApi } from '@/enums/transaction';

type TransactionItemProps = {
  transaction: Transaction;
};

export function TransactionItem({ transaction }: TransactionItemProps) {
  const isIncome = transaction.type === TransactionTypeApi.Income;

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
              bgcolor: `${transaction.categoryColor}22`,
              border: `1px solid ${transaction.categoryColor}33`,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
            }}
          >
            <CategoryIcon
              icon={transaction.categoryIcon}
              sx={{
                fontSize: 21,
                color: transaction.categoryColor,
              }}
            />
          </Box>

          <Box>
            <Typography sx={{ fontWeight: 800 }}>
              {transaction.description}
            </Typography>

            <Typography variant='caption' color='text.secondary'>
              {transaction.categoryName}
            </Typography>
          </Box>
        </Stack>

        <Typography
          sx={{
            fontWeight: 900,
            color: isIncome ? '#55E27A' : '#FF7A7A',
          }}
        >
          {isIncome ? '+' : '-'}{' '}
          {transaction.amount.toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL',
          })}
        </Typography>
      </Stack>
    </Card>
  );
}
