import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  TextField,
  InputAdornment,
} from '@mui/material';

type AccountDialogProps = {
  open: boolean;
  onClose: () => void;
};

const accountTypes = [
  { value: 'checking', label: 'Checking' },
  { value: 'savings', label: 'Savings' },
  { value: 'cash', label: 'Cash' },
  { value: 'investment', label: 'Investment' },
];

export function AccountDialog({ open, onClose }: AccountDialogProps) {
  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    // depois aqui entra o POST /api/accounts
    onClose();
  }

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth='xs'>
      <DialogTitle sx={{ fontWeight: 800 }}>Add Account</DialogTitle>

      <DialogContent sx={{ pt: 1 }}>
        <form onSubmit={handleSubmit}>
          <TextField
            id='account-name'
            name='name'
            label='Account name'
            placeholder='Ex: Nubank, Itaú, Wallet...'
            fullWidth
            required
            margin='normal'
          />

          <TextField
            id='account-type'
            name='type'
            label='Account type'
            select
            fullWidth
            required
            margin='normal'
            defaultValue=''
          >
            {accountTypes.map((type) => (
              <MenuItem key={type.value} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            id='initial-balance'
            name='initialBalance'
            label='Initial balance'
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
