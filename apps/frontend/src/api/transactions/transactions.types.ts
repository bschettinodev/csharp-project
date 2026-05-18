import type { TransactionTypeApi } from '@/enums/transaction';

export type Transaction = {
  id: string;
  description: string;
  amount: number;
  date: string;
  type: TransactionTypeApi;
  accountId: string;
  accountName: string;
  categoryId: string;
  categoryName: string;
  categoryColor: string;
  categoryIcon?: string | null;
  notes?: string | null;
  createdAt: string;
  updatedAt?: string | null;
};
