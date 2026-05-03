import type { TransactionType } from '@/enums/transaction';

export type Category = {
  id: string;
  name: string;
  type: TransactionType;
  color: string;
  icon?: string | null;
};
