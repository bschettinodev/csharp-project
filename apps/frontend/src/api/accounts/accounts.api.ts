import { api } from '@/api/client';

import type { CreateAccountPayload } from './accounts.schema';
import type { Account } from './accounts.types';

export async function getAccounts() {
  const response = await api.get<Account[]>('/accounts');
  return response.data;
}

export async function createAccount(data: CreateAccountPayload) {
  const response = await api.post<Account>('/accounts', data);
  return response.data;
}
