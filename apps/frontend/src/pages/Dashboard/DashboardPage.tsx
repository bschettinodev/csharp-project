import { useState } from 'react';
import AddRoundedIcon from '@mui/icons-material/AddRounded';
import { Box, Stack } from '@mui/material';

import { ActionCard } from '@/components/cards/ActionCard';
import { BalanceSummaryCard } from '@/components/cards/BalanceSummaryCard';
import { TransactionDialog } from '@/components/dialogs/TransactionDialog';
import { TransactionsSection } from '@/components/transactions/TransationcsSection';

import type { TransactionType } from '@/components/dialogs/TransactionDialog';

export function DashboardPage() {
  const [dialogType, setDialogType] = useState<TransactionType>('income');
  const [dialogOpen, setDialogOpen] = useState(false);

  function openTransactionDialog(type: TransactionType) {
    setDialogType(type);
    setDialogOpen(true);
  }

  return (
    <>
      <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2.5 }}>
        <BalanceSummaryCard />

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

        <TransactionsSection />
      </Box>

      <TransactionDialog
        open={dialogOpen}
        type={dialogType}
        onClose={() => setDialogOpen(false)}
      />
    </>
  );
}
