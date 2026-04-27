import { Box, Card, Stack, Typography } from '@mui/material';

type ActionCardProps = {
  title: string;
  color: string;
  icon: React.ReactNode;
  onClick?: () => void;
};

export function ActionCard({ title, color, icon, onClick }: ActionCardProps) {
  return (
    <Card
      onClick={onClick}
      sx={{
        flex: 1,
        height: 56,
        px: 2,
        borderRadius: '999px',
        bgcolor: 'background.paper',
        color: 'common.white',
        backgroundImage: 'none',
        boxShadow: 'none',
        border: '1px solid rgba(255,255,255,0.06)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        cursor: 'pointer',
        transition: 'all 0.2s ease',
        '&:active': {
          transform: 'scale(0.97)',
        },
      }}
    >
      <Stack direction='row' sx={{ alignItems: 'center', gap: 1 }}>
        <Box
          sx={{
            width: 28,
            height: 28,
            borderRadius: '50%',
            bgcolor: color,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
          }}
        >
          {icon}
        </Box>

        <Typography
          sx={{
            fontSize: 17,
            fontWeight: 700,
            color: 'common.white',
          }}
        >
          {title}
        </Typography>
      </Stack>
    </Card>
  );
}
