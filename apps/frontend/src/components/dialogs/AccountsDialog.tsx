import { zodResolver } from '@hookform/resolvers/zod';
import {
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  InputAdornment,
  MenuItem,
  TextField,
} from '@mui/material';
import { useState } from 'react';
import { useForm } from 'react-hook-form';

import { createAccount } from '@/api/accounts/accounts.api';
import {
  createAccountDefaults,
  createAccountSchema,
  type CreateAccountFormValues,
  type CreateAccountPayload,
} from '@/api/accounts/accounts.schema';
import { accountTypeOptions } from '@/enums/account';

type AccountDialogProps = {
  open: boolean;
  onClose: () => void;
  onCreated?: () => void;
};

export function AccountDialog({
  open,
  onClose,
  onCreated,
}: AccountDialogProps) {
  const [isSaving, setIsSaving] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateAccountFormValues, unknown, CreateAccountPayload>({
    resolver: zodResolver(createAccountSchema),
    defaultValues: createAccountDefaults,
  });

  function handleDialogClose() {
    reset();
    setErrorMessage('');
    onClose();
  }

  async function onSubmit(data: CreateAccountPayload) {
    setIsSaving(true);
    setErrorMessage('');

    try {
      await createAccount(data);
      onCreated?.();
      handleDialogClose();
    } catch (error) {
      console.error(error);
      setErrorMessage('Could not create account. Please try again.');
    } finally {
      setIsSaving(false);
    }
  }

  return (
    <Dialog open={open} onClose={handleDialogClose} fullWidth maxWidth='xs'>
      <DialogTitle sx={{ fontWeight: 800 }}>Add Account</DialogTitle>

      <DialogContent sx={{ pt: 1 }}>
        {errorMessage && <Alert severity='error'>{errorMessage}</Alert>}

        <form onSubmit={handleSubmit(onSubmit)}>
          <TextField
            {...register('name')}
            id='account-name'
            label='Account name'
            placeholder='Ex: Nubank, Itaú, Wallet...'
            fullWidth
            required
            margin='normal'
            error={!!errors.name}
            helperText={errors.name?.message}
            disabled={isSaving}
          />

          <TextField
            {...register('type')}
            id='account-type'
            label='Account type'
            select
            fullWidth
            required
            margin='normal'
            defaultValue=''
            error={!!errors.type}
            helperText={errors.type?.message}
            disabled={isSaving}
          >
            {accountTypeOptions.map((type) => (
              <MenuItem key={type.value} value={String(type.value)}>
                {type.label}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            {...register('initialBalance')}
            id='initial-balance'
            label='Initial balance'
            placeholder='0.00'
            fullWidth
            required
            margin='normal'
            error={!!errors.initialBalance}
            helperText={errors.initialBalance?.message}
            disabled={isSaving}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position='start'>R$</InputAdornment>
                ),
              },
              htmlInput: {
                inputMode: 'decimal',
              },
            }}
          />

          <DialogActions sx={{ px: 0, pt: 2 }}>
            <Button onClick={handleDialogClose} disabled={isSaving}>
              Cancel
            </Button>
            <Button type='submit' variant='contained' disabled={isSaving}>
              {isSaving ? 'Saving...' : 'Save'}
            </Button>
          </DialogActions>
        </form>
      </DialogContent>
    </Dialog>
  );
}
