import { useCallback, useEffect, useMemo, useState } from 'react';

import {
  Alert,
  Autocomplete,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  InputAdornment,
  TextField,
} from '@mui/material';

import { Controller, useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

import dayjs from 'dayjs';

import { getAccounts } from '@/api/accounts/accounts.api';
import type { Account } from '@/api/accounts/accounts.types';
import { getCategories } from '@/api/categories/categories.api';
import type { Category } from '@/api/categories/categories.types';
import { createTransaction } from '@/api/transactions/transactions.api';

import {
  createTransactionSchema,
  createTransactionDefaults,
  type CreateTransactionFormValues,
  type CreateTransactionPayload,
} from '@/api/transactions/transactions.schema';

import {
  TransactionTypeApi,
  transactionTypeApiMap,
  TransactionType as TransactionTypeEnum,
} from '@/enums/transaction';

export type TransactionDialogType = 'income' | 'expense';

type TransactionDialogProps = {
  open: boolean;
  type: TransactionDialogType;
  onClose: () => void;
  onCreated?: () => void;
};

export function TransactionDialog({
  open,
  type,
  onClose,
  onCreated,
}: TransactionDialogProps) {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [isSaving, setIsSaving] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const isIncome = type === 'income';

  const {
    register,
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateTransactionFormValues, unknown, CreateTransactionPayload>({
    resolver: zodResolver(createTransactionSchema),
    defaultValues: {
      ...createTransactionDefaults,
      type: isIncome ? TransactionTypeApi.Income : TransactionTypeApi.Expense,
    },
  });

  const handleDialogClose = useCallback(() => {
    reset();
    setErrorMessage('');
    onClose();
  }, [onClose, reset]);

  const fetchDialogData = useCallback(async () => {
    const [accountsData, categoriesData] = await Promise.all([
      getAccounts(),
      getCategories(),
    ]);

    setAccounts(accountsData);
    setCategories(categoriesData);
  }, []);

  useEffect(() => {
    if (open) {
      fetchDialogData();
      reset({
        ...createTransactionDefaults,
        type: isIncome ? TransactionTypeApi.Income : TransactionTypeApi.Expense,
        date: dayjs(),
      });
    }
  }, [open, fetchDialogData, reset, isIncome]);

  const filteredCategories = useMemo(() => {
    const transactionType = isIncome
      ? TransactionTypeEnum.Income
      : TransactionTypeEnum.Expense;

    return categories.filter(
      (category) => transactionTypeApiMap[category.type] === transactionType
    );
  }, [categories, isIncome]);

  async function onSubmit(data: CreateTransactionPayload) {
    console.log(data);
    setIsSaving(true);
    setErrorMessage('');

    try {
      await createTransaction(data);
      onCreated?.();
      handleDialogClose();
    } catch (error) {
      console.error(error);
      setErrorMessage('Could not create transaction. Please try again.');
    } finally {
      setIsSaving(false);
    }
  }

  return (
    <Dialog open={open} onClose={handleDialogClose} fullWidth maxWidth='xs'>
      <DialogTitle sx={{ fontWeight: 800 }}>
        {isIncome ? 'Add Income' : 'Add Expense'}
      </DialogTitle>

      <DialogContent sx={{ pt: 1 }}>
        {errorMessage && <Alert severity='error'>{errorMessage}</Alert>}

        <form
          onSubmit={handleSubmit(onSubmit, (formErrors) => {
            console.log(formErrors);
          })}
        >
          <TextField
            {...register('description')}
            label='Description'
            fullWidth
            required
            margin='normal'
            error={!!errors.description}
            helperText={errors.description?.message}
            disabled={isSaving}
          />
          <TextField
            {...register('amount')}
            label='Amount'
            placeholder='0.00'
            fullWidth
            required
            margin='normal'
            error={!!errors.amount}
            helperText={errors.amount?.message}
            disabled={isSaving}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position='start'>R$</InputAdornment>
                ),
              },
            }}
          />
          <Controller
            name='date'
            control={control}
            render={({ field }) => (
              <DatePicker
                label='Date'
                value={field.value}
                onChange={field.onChange}
                disabled={isSaving}
                slotProps={{
                  textField: {
                    fullWidth: true,
                    margin: 'normal',
                    error: !!errors.date,
                    helperText: errors.date?.message,
                  },
                }}
              />
            )}
          />
          <Controller
            name='accountId'
            control={control}
            render={({ field }) => {
              const selectedAccount =
                accounts.find((account) => account.id === field.value) ?? null;

              return (
                <Autocomplete
                  options={accounts}
                  value={selectedAccount}
                  onChange={(_, newValue) => {
                    field.onChange(newValue?.id ?? '');
                  }}
                  getOptionLabel={(option) => option.name}
                  isOptionEqualToValue={(option, value) =>
                    option.id === value.id
                  }
                  disabled={isSaving}
                  fullWidth
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      label='Account'
                      required
                      margin='normal'
                      error={!!errors.accountId}
                      helperText={errors.accountId?.message}
                    />
                  )}
                />
              );
            }}
          />
          <Controller
            name='categoryId'
            control={control}
            render={({ field }) => {
              const selectedCategory =
                filteredCategories.find(
                  (category) => category.id === field.value
                ) ?? null;

              return (
                <Autocomplete
                  options={filteredCategories}
                  value={selectedCategory}
                  onChange={(_, newValue) => {
                    field.onChange(newValue?.id ?? '');
                  }}
                  getOptionLabel={(option) => option.name}
                  isOptionEqualToValue={(option, value) =>
                    option.id === value.id
                  }
                  disabled={isSaving}
                  fullWidth
                  renderOption={(props, option) => (
                    <Box
                      component='li'
                      {...props}
                      key={option.id}
                      sx={{
                        display: 'flex',
                        alignItems: 'center',
                        gap: 1,
                      }}
                    >
                      <Box
                        sx={{
                          width: 32,
                          height: 32,
                          borderRadius: 2,
                          backgroundColor: option.color,
                          display: 'flex',
                          alignItems: 'center',
                          justifyContent: 'center',
                          color: 'white',
                          fontWeight: 700,
                          fontSize: 12,
                        }}
                      >
                        {option.name.charAt(0)}
                      </Box>

                      {option.name}
                    </Box>
                  )}
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      label='Category'
                      required
                      margin='normal'
                      error={!!errors.categoryId}
                      helperText={errors.categoryId?.message}
                    />
                  )}
                />
              );
            }}
          />
          <TextField
            {...register('notes')}
            label='Notes'
            fullWidth
            margin='normal'
            multiline
            minRows={3}
            disabled={isSaving}
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
