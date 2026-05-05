import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ConfigProvider } from 'antd';
import { useAuthStore } from './store/authStore';
import AppLayout from './components/layout/AppLayout';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import AdminsPage from './pages/AdminsPage';
import AdminDetailPage from './pages/AdminDetailPage';
import TemplatesPage from './pages/TemplatesPage';
import YearlyOrdersPage from './pages/orders/YearlyOrdersPage';
import MonthlyOrdersPage from './pages/orders/MonthlyOrdersPage';
import SafetyOrdersPage from './pages/orders/SafetyOrdersPage';

const qc = new QueryClient({ defaultOptions: { queries: { retry: 1, staleTime: 30_000 } } });

function RequireAuth({ children }: { children: React.ReactNode }) {
  const token = useAuthStore((s) => s.token);
  return token ? <>{children}</> : <Navigate to="/" replace />;
}

export default function App() {
  return (
    <QueryClientProvider client={qc}>
      <ConfigProvider theme={{ token: { colorPrimary: '#1677ff' } }}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route
              element={
                <RequireAuth>
                  <AppLayout />
                </RequireAuth>
              }
            >
              <Route path="/dashboard" element={<DashboardPage />} />
              <Route path="/admins" element={<AdminsPage />} />
              <Route path="/admins/:id" element={<AdminDetailPage />} />
              <Route path="/templates" element={<TemplatesPage />} />
              <Route path="/orders/yearly" element={<YearlyOrdersPage />} />
              <Route path="/orders/monthly" element={<MonthlyOrdersPage />} />
              <Route path="/orders/safety" element={<SafetyOrdersPage />} />
            </Route>
          </Routes>
        </BrowserRouter>
      </ConfigProvider>
    </QueryClientProvider>
  );
}
