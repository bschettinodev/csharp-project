import { api } from '@/api/client';

import type { Category } from './categories.types';

export async function getCategories() {
  const response = await api.get<Category[]>('/categories');
  return response.data;
}
