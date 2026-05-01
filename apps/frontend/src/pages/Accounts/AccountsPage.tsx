import { useState } from 'react';

import AddRoundedIcon from '@mui/icons-material/AddRounded';
import { Box, IconButton, Stack, Typography } from '@mui/material';

import { AccountCard } from '@/components/cards/AccountCard';
import { AccountDialog } from '@/components/dialogs/AccountsDialog';

const accounts = [
  { name: 'Nubank', type: 'Checking', balance: 'R$ 2.100,00' },
  { name: 'Itaú', type: 'Savings', balance: 'R$ 1.800,00' },
  { name: 'Wallet', type: 'Cash', balance: 'R$ 350,00' },
];

export function AccountsPage() {
  const [accountDialogOpen, setAccountDialogOpen] = useState(false);

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

      <CardSummary />

      <Stack sx={{ gap: 1.5 }}>
        {accounts.map((account) => (
          <AccountCard key={account.name} {...account} />
        ))}
      </Stack>

      <AccountDialog
        open={accountDialogOpen}
        onClose={() => setAccountDialogOpen(false)}
      />
    </Box>
  );
}

function CardSummary() {
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
        R$ 4.250,00
      </Typography>
    </Box>
  );
}
