export const TransactionType = {
  Income: 1,
  Expense: 2,
} as const;

export type TransactionType =
  (typeof TransactionType)[keyof typeof TransactionType];

export const TransactionTypeApi = {
  Income: 'Income',
  Expense: 'Expense',
} as const;

export type TransactionTypeApi =
  (typeof TransactionTypeApi)[keyof typeof TransactionTypeApi];

export const transactionTypeApiMap: Record<
  TransactionTypeApi,
  TransactionType
> = {
  Income: TransactionType.Income,
  Expense: TransactionType.Expense,
};
