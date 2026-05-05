import { Layout, Menu, Button, Typography, Avatar, Space } from 'antd';
import {
  UserOutlined,
  TeamOutlined,
  SafetyCertificateOutlined,
  FileTextOutlined,
  LogoutOutlined,
  DashboardOutlined,
  ApartmentOutlined,
} from '@ant-design/icons';
import { Link, Outlet, useNavigate, useLocation } from 'react-router-dom';
import { useAuthStore } from '../../store/authStore';

const { Header, Sider, Content } = Layout;

const menuItems = [
  { key: '/dashboard', label: <Link to="/dashboard">Dashboard</Link>, icon: <DashboardOutlined /> },
  { key: '/admins', label: <Link to="/admins">Admins</Link>, icon: <TeamOutlined /> },
  { key: '/templates', label: <Link to="/templates">Permission Templates</Link>, icon: <ApartmentOutlined /> },
  {
    key: 'orders',
    label: 'Orders',
    icon: <FileTextOutlined />,
    children: [
      { key: '/orders/yearly', label: <Link to="/orders/yearly">Yearly Orders</Link> },
      { key: '/orders/monthly', label: <Link to="/orders/monthly">Monthly Orders</Link> },
      { key: '/orders/safety', label: <Link to="/orders/safety">Safety Check Orders</Link> },
    ],
  },
];

export default function AppLayout() {
  const { name, clearAuth } = useAuthStore();
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    clearAuth();
    navigate('/');
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider width={240} theme="dark">
        <div style={{ padding: '16px', borderBottom: '1px solid rgba(255,255,255,0.1)' }}>
          <Typography.Text strong style={{ color: '#fff', fontSize: 16 }}>
            <SafetyCertificateOutlined style={{ marginRight: 8 }} />
            Permission Manager
          </Typography.Text>
        </div>
        <Menu
          theme="dark"
          mode="inline"
          selectedKeys={[location.pathname]}
          defaultOpenKeys={['orders']}
          items={menuItems}
          style={{ marginTop: 8 }}
        />
      </Sider>
      <Layout>
        <Header style={{ background: '#fff', padding: '0 24px', display: 'flex', alignItems: 'center', justifyContent: 'flex-end', boxShadow: '0 1px 4px rgba(0,0,0,0.1)' }}>
          <Space>
            <Avatar icon={<UserOutlined />} />
            <Typography.Text strong>{name}</Typography.Text>
            <Button icon={<LogoutOutlined />} onClick={handleLogout} type="text">Logout</Button>
          </Space>
        </Header>
        <Content style={{ margin: 24, padding: 24, background: '#f5f5f5', minHeight: 280 }}>
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
}
