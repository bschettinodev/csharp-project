import { Box, Card, CardContent, Grid, Typography } from '@mui/material';

const cards = [
  { title: 'Total Balance', value: 'R$ 4.250,00' },
  { title: 'Income', value: 'R$ 6.000,00' },
  { title: 'Expenses', value: 'R$ 1.750,00' },
];

export function DashboardPage() {
  return (
    <Box>
      <Typography variant='h4' sx={{ fontWeight: 700, mb: 3 }}>
        Dashboard
      </Typography>

      <Grid container spacing={3}>
        {cards.map((card) => (
          <Grid key={card.title} size={{ xs: 12, md: 4 }}>
            <Card sx={{ bgcolor: '#0f1d33', borderRadius: 4 }}>
              <CardContent>
                <Typography sx={{ color: 'rgba(255,255,255,0.6)', mb: 1 }}>
                  {card.title}
                </Typography>

                <Typography variant='h4' sx={{ fontWeight: 700 }}>
                  {card.value}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
