import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import VisibilityOffRoundedIcon from '@mui/icons-material/VisibilityOffRounded';
import VisibilityRoundedIcon from '@mui/icons-material/VisibilityRounded';
import {
  Alert,
  Box,
  Button,
  Card,
  CircularProgress,
  FormControl,
  FormLabel,
  IconButton,
  InputAdornment,
  Stack,
  TextField,
  Typography,
} from '@mui/material';

import { firebase } from '@/firebase/app';

export function LoginPage() {
  const navigate = useNavigate();

  const [showPassword, setShowPassword] = useState(false);
  const [form, setForm] = useState({ email: '', password: '' });
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setLoading(true);
    setErrorMessage('');

    try {
      await firebase().handlers.signIn({
        email: form.email,
        password: form.password,
      });

      navigate('/dashboard');
    } catch (error) {
      console.error(error);
      setErrorMessage('Invalid email or password');
    } finally {
      setLoading(false);
    }
  }

  return (
    <Box
      sx={{
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        px: 2,
        bgcolor: '#07111f',
        color: 'common.white',
        background:
          'radial-gradient(circle at top right, rgba(34, 197, 94, 0.12), transparent 32%), #07111f',
      }}
    >
      <Card
        component='form'
        onSubmit={handleSubmit}
        sx={{
          width: '100%',
          maxWidth: 440,
          p: { xs: 3, sm: 4 },
          borderRadius: 4,
          bgcolor: '#0b1628',
          border: '1px solid rgba(255,255,255,0.08)',
          boxShadow: '0 24px 80px rgba(0,0,0,0.35)',
        }}
      >
        <Stack sx={{ gap: 3 }}>
          <Box>
            <Typography variant='h4' sx={{ fontWeight: 800, mb: 1 }}>
              Sign in
            </Typography>

            <Typography
              variant='body2'
              sx={{ color: 'rgba(255,255,255,0.56)' }}
            >
              Access your personal finance dashboard.
            </Typography>
          </Box>

          {errorMessage && <Alert severity='info'>{errorMessage}</Alert>}

          <FormControl fullWidth>
            <FormLabel sx={{ mb: 1, color: 'rgba(255,255,255,0.72)' }}>
              Email address
            </FormLabel>

            <TextField
              value={form.email}
              onChange={(event) =>
                setForm((current) => ({
                  ...current,
                  email: event.target.value,
                }))
              }
              placeholder='your@email.com'
              type='email'
              autoComplete='email'
              fullWidth
            />
          </FormControl>

          <FormControl fullWidth>
            <FormLabel sx={{ mb: 1, color: 'rgba(255,255,255,0.72)' }}>
              Password
            </FormLabel>

            <TextField
              value={form.password}
              onChange={(event) =>
                setForm((current) => ({
                  ...current,
                  password: event.target.value,
                }))
              }
              placeholder='••••••••'
              type={showPassword ? 'text' : 'password'}
              autoComplete='current-password'
              fullWidth
              slotProps={{
                input: {
                  endAdornment: (
                    <InputAdornment position='end'>
                      <IconButton
                        edge='end'
                        onClick={() => setShowPassword((current) => !current)}
                      >
                        {showPassword ? (
                          <VisibilityRoundedIcon />
                        ) : (
                          <VisibilityOffRoundedIcon />
                        )}
                      </IconButton>
                    </InputAdornment>
                  ),
                },
              }}
            />
          </FormControl>

          <Button
            type='submit'
            variant='contained'
            size='large'
            disabled={loading}
            sx={{
              py: 1.3,
              borderRadius: 3,
              fontWeight: 700,
            }}
          >
            {loading ? <CircularProgress size={22} /> : 'Sign in'}
          </Button>
        </Stack>
      </Card>
    </Box>
  );
}
