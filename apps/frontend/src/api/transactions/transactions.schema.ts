import dayjs, { type Dayjs } from 'dayjs';
import { z } from 'zod';

import { TransactionTypeApi } from '@/enums/transaction';

export const createTransactionSchema = z.object({
  description: z.string().min(1, 'Description is required'),

  amount: z
    .string()
    .min(1, 'Amount is required')
    .transform(Number)
    .refine((value) => value > 0, 'Amount must be greater than zero'),

  date: z
    .custom<Dayjs>((value) => dayjs.isDayjs(value) && value.isValid(), {
      message: 'Date is required',
    })
    .transform((value) => value.toISOString()),

  type: z.union([
    z.literal(TransactionTypeApi.Income),
    z.literal(TransactionTypeApi.Expense),
  ]),

  accountId: z.uuid('Account is required'),
  categoryId: z.uuid('Category is required'),
  notes: z.string().optional(),
});

export type CreateTransactionFormValues = z.input<
  typeof createTransactionSchema
>;

export type CreateTransactionPayload = z.output<typeof createTransactionSchema>;

export const createTransactionDefaults: CreateTransactionFormValues = {
  description: '',
  amount: '',
  date: dayjs(),
  type: TransactionTypeApi.Expense,
  accountId: '',
  categoryId: '',
  notes: '',
};
