import RestaurantRoundedIcon from '@mui/icons-material/RestaurantRounded';
import DirectionsCarRoundedIcon from '@mui/icons-material/DirectionsCarRounded';
import ShoppingBagRoundedIcon from '@mui/icons-material/ShoppingBagRounded';
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import ReceiptRoundedIcon from '@mui/icons-material/ReceiptRounded';
import FavoriteRoundedIcon from '@mui/icons-material/FavoriteRounded';
import SchoolRoundedIcon from '@mui/icons-material/SchoolRounded';
import MovieRoundedIcon from '@mui/icons-material/MovieRounded';
import SubscriptionsRoundedIcon from '@mui/icons-material/SubscriptionsRounded';
import LocalGroceryStoreRoundedIcon from '@mui/icons-material/LocalGroceryStoreRounded';
import FlightRoundedIcon from '@mui/icons-material/FlightRounded';
import SpaRoundedIcon from '@mui/icons-material/SpaRounded';
import PetsRoundedIcon from '@mui/icons-material/PetsRounded';
import AccountBalanceRoundedIcon from '@mui/icons-material/AccountBalanceRounded';
import WorkRoundedIcon from '@mui/icons-material/WorkRounded';
import TrendingUpRoundedIcon from '@mui/icons-material/TrendingUpRounded';
import PaidRoundedIcon from '@mui/icons-material/PaidRounded';

export const categoryIconMap = {
  food: RestaurantRoundedIcon,
  transport: DirectionsCarRoundedIcon,
  shopping: ShoppingBagRoundedIcon,
  housing: HomeRoundedIcon,
  bills: ReceiptRoundedIcon,
  health: FavoriteRoundedIcon,
  education: SchoolRoundedIcon,
  entertainment: MovieRoundedIcon,
  subscriptions: SubscriptionsRoundedIcon,
  groceries: LocalGroceryStoreRoundedIcon,
  travel: FlightRoundedIcon,
  'personal-care': SpaRoundedIcon,
  pets: PetsRoundedIcon,
  taxes: AccountBalanceRoundedIcon,

  salary: WorkRoundedIcon,
  freelance: PaidRoundedIcon,
  investment: TrendingUpRoundedIcon,
  refund: PaidRoundedIcon,
} as const;

export function getCategoryIcon(icon?: string | null) {
  if (!icon) return null;

  return categoryIconMap[icon as keyof typeof categoryIconMap] ?? null;
}
