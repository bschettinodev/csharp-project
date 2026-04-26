import { DashboardLayout } from '@/components/Layout/DashboardLayout/DashboardLayout';
import { DashboardPage } from '@/pages/Dashboard/DashboardPage';

export default function App() {
  return (
    <DashboardLayout>
      <DashboardPage />
    </DashboardLayout>
  );
}
