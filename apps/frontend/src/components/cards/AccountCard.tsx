import { Card, Stack, Typography } from '@mui/material';

type AccountCardProps = {
  name: string;
  type: string;
  balance: string;
};

export function AccountCard({ name, type, balance }: AccountCardProps) {
  return (
    <Card
      sx={{
        p: 2,
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
        <Stack>
          <Typography sx={{ fontWeight: 800 }}>{name}</Typography>
          <Typography variant='caption' color='text.secondary'>
            {type}
          </Typography>
        </Stack>

        <Typography sx={{ fontWeight: 900 }}>{balance}</Typography>
      </Stack>
    </Card>
  );
}
