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

export type TransactionType = 'income' | 'expense';

type TransactionDialogProps = {
  open: boolean;
  type: TransactionType;
  onClose: () => void;
};

const accounts = [
  { id: '1', name: 'Nubank' },
  { id: '2', name: 'Wallet' },
];

const categories = {
  income: [
    { id: '1', name: 'Salary' },
    { id: '2', name: 'Freelance' },
  ],
  expense: [
    { id: '3', name: 'Food' },
    { id: '4', name: 'Transport' },
    { id: '5', name: 'Shopping' },
  ],
};

export function TransactionDialog({
  open,
  type,
  onClose,
}: TransactionDialogProps) {
  const isIncome = type === 'income';

  function handleSubmit(event: React.ChangeEvent<HTMLFormElement>) {
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
            {categories[type].map((category) => (
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
