import AddRoundedIcon from '@mui/icons-material/AddRounded';
import { Box, Stack } from '@mui/material';

import { ActionCard } from '@/components/cards/ActionCard';
import { BalanceSummaryCard } from '@/components/cards/BalanceSummaryCard';
import { TransactionsSection } from '@/components/transactions/TransationcsSection';

export function DashboardPage() {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2.5 }}>
      <BalanceSummaryCard />

      <Stack direction='row' sx={{ gap: 2 }}>
        <ActionCard
          title='Add Income'
          color='#55E27A'
          icon={<AddRoundedIcon sx={{ color: '#000', fontSize: 18 }} />}
        />

        <ActionCard
          title='Add Expense'
          color='#FF7A7A'
          icon={<AddRoundedIcon sx={{ color: '#000', fontSize: 18 }} />}
        />
      </Stack>

      <TransactionsSection />
    </Box>
  );
}
