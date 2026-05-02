import { useCallback, useEffect, useState } from 'react';

import AddRoundedIcon from '@mui/icons-material/AddRounded';
import { Box, IconButton, Stack, Typography } from '@mui/material';

import { getAccounts } from '@/api/accounts/accounts.api';
import type { Account } from '@/api/accounts/accounts.types';
import { AccountCard } from '@/components/cards/AccountCard';
import { AccountDialog } from '@/components/dialogs/AccountsDialog';
import { accountTypeLabels } from '@/enums/account';

export function AccountsPage() {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [accountDialogOpen, setAccountDialogOpen] = useState(false);

  const fetchAccounts = useCallback(async () => {
    const data = await getAccounts();
    setAccounts(data);
  }, []);

  useEffect(() => {
    fetchAccounts();
  }, [fetchAccounts]);

  const totalBalance = accounts.reduce(
    (total, account) => total + account.currentBalance,
    0
  );

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2.5 }}>
      <Stack
        direction='row'
        sx={{
          alignItems: 'center',
          justifyContent: 'space-between',
        }}
      >
        <Box>
          <Typography variant='h5' sx={{ fontWeight: 900 }}>
            Accounts
          </Typography>

          <Typography variant='body2' color='text.secondary'>
            Manage where your money is stored.
          </Typography>
        </Box>

        <IconButton
          onClick={() => setAccountDialogOpen(true)}
          sx={{
            bgcolor: 'primary.main',
            color: 'primary.contrastText',
            width: 42,
            height: 42,
            '&:hover': {
              bgcolor: 'primary.main',
            },
          }}
        >
          <AddRoundedIcon />
        </IconButton>
      </Stack>

      <CardSummary totalBalance={totalBalance} />

      <Stack sx={{ gap: 1.5 }}>
        {accounts.map((account) => (
          <AccountCard
            key={account.id}
            name={account.name}
            type={accountTypeLabels[account.type]}
            balance={account.currentBalance.toLocaleString('pt-BR', {
              style: 'currency',
              currency: 'BRL',
            })}
          />
        ))}
      </Stack>

      <AccountDialog
        open={accountDialogOpen}
        onClose={() => setAccountDialogOpen(false)}
        onCreated={fetchAccounts}
      />
    </Box>
  );
}

type CardSummaryProps = {
  totalBalance: number;
};

function CardSummary({ totalBalance }: CardSummaryProps) {
  return (
    <Box
      sx={{
        p: 2.5,
        borderRadius: 5,
        bgcolor: 'primary.main',
        color: 'primary.contrastText',
      }}
    >
      <Typography variant='body2' sx={{ fontWeight: 700, opacity: 0.7 }}>
        Total across accounts
      </Typography>

      <Typography sx={{ fontSize: 32, fontWeight: 900, mt: 0.5 }}>
        {totalBalance.toLocaleString('pt-BR', {
          style: 'currency',
          currency: 'BRL',
        })}
      </Typography>
    </Box>
  );
}
