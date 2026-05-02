export const AccountType = {
  Checking: 1,
  Savings: 2,
  Wallet: 3,
  CreditCard: 4,
} as const;

export type AccountType = (typeof AccountType)[keyof typeof AccountType];

export const accountTypeLabels: Record<AccountType, string> = {
  [AccountType.Checking]: 'Checking',
  [AccountType.Savings]: 'Savings',
  [AccountType.Wallet]: 'Wallet',
  [AccountType.CreditCard]: 'Credit Card',
};

export const accountTypeOptions = Object.values(AccountType).map((value) => ({
  value,
  label: accountTypeLabels[value],
}));
