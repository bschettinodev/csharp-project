import type { SvgIconProps } from '@mui/material/SvgIcon';

import { getCategoryIcon } from '@/utils/categoryIcons';

type CategoryIconProps = {
  icon?: string | null;
} & SvgIconProps;

export function CategoryIcon({ icon, ...props }: CategoryIconProps) {
  const IconComponent = getCategoryIcon(icon);

  if (!IconComponent) {
    return null;
  }

  // eslint-disable-next-line react-hooks/static-components
  return <IconComponent {...props} />;
}
