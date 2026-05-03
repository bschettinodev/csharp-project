import { useCallback, useEffect, useMemo, useState } from 'react';

import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  InputAdornment,
  MenuItem,
  TextField,
} from '@mui/material';

import { getAccounts } from '@/api/accounts/accounts.api';
import type { Account } from '@/api/accounts/accounts.types';
import { getCategories } from '@/api/categories/categories.api';
import type { Category } from '@/api/categories/categories.types';
import { TransactionType as TransactionTypeEnum } from '@/enums/transaction';

export type TransactionDialogType = 'income' | 'expense';

type TransactionDialogProps = {
  open: boolean;
  type: TransactionDialogType;
  onClose: () => void;
};

export function TransactionDialog({
  open,
  type,
  onClose,
}: TransactionDialogProps) {
  const [accounts, setAccounts] = useState<Account[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);

  const isIncome = type === 'income';

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
    }
  }, [open, fetchDialogData]);

  const filteredCategories = useMemo(() => {
    const transactionType = isIncome
      ? TransactionTypeEnum.Income
      : TransactionTypeEnum.Expense;

    return categories.filter((category) => category.type === transactionType);
  }, [categories, isIncome]);

  function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
    event.preventDefault();
    onClose();
  }

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth='xs'>
      <DialogTitle sx={{ fontWeight: 800 }}>
        {isIncome ? 'Add Income' : 'Add Expense'}
      </DialogTitle>

      <DialogContent sx={{ pt: 1 }}>
        <form onSubmit={handleSubmit}>
          <TextField
            id='transaction-description'
            name='description'
            label='Description'
            placeholder='Ex: Salary, Market, Netflix...'
            fullWidth
            required
            margin='normal'
          />

          <TextField
            id='transaction-amount'
            name='amount'
            label='Amount'
            placeholder='0.00'
            fullWidth
            required
            margin='normal'
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

          <TextField
            id='transaction-date'
            name='date'
            label='Date'
            type='date'
            fullWidth
            required
            margin='normal'
            slotProps={{
              inputLabel: {
                shrink: true,
              },
            }}
          />

          <TextField
            id='transaction-account'
            name='accountId'
            label='Account'
            select
            fullWidth
            required
            margin='normal'
            defaultValue=''
            helperText='Select the account affected by this transaction'
          >
            {accounts.map((account) => (
              <MenuItem key={account.id} value={account.id}>
                {account.name}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            id='transaction-category'
            name='categoryId'
            label='Category'
            select
            fullWidth
            required
            margin='normal'
            defaultValue=''
            helperText={`Only ${isIncome ? 'income' : 'expense'} categories are shown`}
          >
            {filteredCategories.map((category) => (
              <MenuItem key={category.id} value={category.id}>
                {category.name}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            id='transaction-notes'
            name='notes'
            label='Notes'
            placeholder='Optional notes...'
            fullWidth
            margin='normal'
            multiline
            minRows={3}
          />

          <DialogActions sx={{ px: 0, pt: 2 }}>
            <Button onClick={onClose}>Cancel</Button>
            <Button type='submit' variant='contained'>
              Save
            </Button>
          </DialogActions>
        </form>
      </DialogContent>
    </Dialog>
  );
}
