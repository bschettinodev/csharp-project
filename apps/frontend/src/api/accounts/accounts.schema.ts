import { z } from 'zod';

import { AccountType } from '@/enums/account';

export const createAccountSchema = z.object({
  name: z.string().min(2, 'Name must have at least 2 characters'),

  type: z
    .string()
    .min(1, 'Account type is required')
    .transform(Number)
    .refine(
      (value) => Object.values(AccountType).includes(value as AccountType),
      'Account type is invalid'
    ),

  initialBalance: z
    .string()
    .min(1, 'Initial balance is required')
    .transform(Number)
    .refine((value) => value >= 0, 'Initial balance cannot be negative'),
});

export type CreateAccountFormValues = z.input<typeof createAccountSchema>;
export type CreateAccountPayload = z.output<typeof createAccountSchema>;

export const createAccountDefaults: CreateAccountFormValues = {
  name: '',
  type: '',
  initialBalance: '',
};
