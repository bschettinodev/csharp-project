import type { TransactionTypeApi } from '@/enums/transaction';

export type Category = {
  id: string;
  name: string;
  type: TransactionTypeApi;
  color: string;
  icon: string;
};
