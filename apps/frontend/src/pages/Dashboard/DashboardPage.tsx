import { useCallback, useEffect, useMemo, useState } from 'react';
import AddRoundedIcon from '@mui/icons-material/AddRounded';
import { Box, Stack } from '@mui/material';

import { getTransactions } from '@/api/transactions/transactions.api';
import type { Transaction } from '@/api/transactions/transactions.types';
import { ActionCard } from '@/components/cards/ActionCard';
import { BalanceSummaryCard } from '@/components/cards/BalanceSummaryCard';
import {
  TransactionDialog,
  type TransactionDialogType,
} from '@/components/dialogs/TransactionDialog';
import { TransactionsSection } from '@/components/transactions/TransationcsSection';
import { TransactionTypeApi } from '@/enums/transaction';

export function DashboardPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [dialogType, setDialogType] = useState<TransactionDialogType>('income');
  const [dialogOpen, setDialogOpen] = useState(false);

  const fetchTransactions = useCallback(async () => {
    const data = await getTransactions();
    setTransactions(data);
  }, []);

  useEffect(() => {
    fetchTransactions();
  }, [fetchTransactions]);

  const { totalIncome, totalExpenses, totalBalance } = useMemo(() => {
    const totalIncome = transactions
      .filter((transaction) => transaction.type === TransactionTypeApi.Income)
      .reduce((total, transaction) => total + transaction.amount, 0);

    const totalExpenses = transactions
      .filter((transaction) => transaction.type === TransactionTypeApi.Expense)
      .reduce((total, transaction) => total + transaction.amount, 0);

    return {
      totalIncome,
      totalExpenses,
      totalBalance: totalIncome - totalExpenses,
    };
  }, [transactions]);

  function openTransactionDialog(type: TransactionDialogType) {
    setDialogType(type);
    setDialogOpen(true);
  }

  return (
    <>
      <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2.5 }}>
        <BalanceSummaryCard
          totalBalance={totalBalance}
          totalIncome={totalIncome}
          totalExpenses={totalExpenses}
        />

        <Stack direction='row' sx={{ gap: 2 }}>
          <ActionCard
            title='Add Income'
            color='#55E27A'
            icon={<AddRoundedIcon sx={{ color: '#000', fontSize: 18 }} />}
            onClick={() => openTransactionDialog('income')}
          />

          <ActionCard
            title='Add Expense'
            color='#FF7A7A'
            icon={<AddRoundedIcon sx={{ color: '#000', fontSize: 18 }} />}
            onClick={() => openTransactionDialog('expense')}
          />
        </Stack>

        <TransactionsSection transactions={transactions} />
      </Box>

      <TransactionDialog
        open={dialogOpen}
        type={dialogType}
        onClose={() => setDialogOpen(false)}
        onCreated={fetchTransactions}
      />
    </>
  );
}
