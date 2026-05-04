import { api } from '@/api/client';

import type { CreateTransactionPayload } from './transactions.schema';
import type { Transaction } from './transactions.types';

export async function getTransactions() {
  const response = await api.get<Transaction[]>('/transactions');
  return response.data;
}

export async function createTransaction(data: CreateTransactionPayload) {
  const response = await api.post<Transaction>('/transactions', data);
  return response.data;
}
