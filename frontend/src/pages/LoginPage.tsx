import { Button, Card, Form, Input, Typography, message } from 'antd';
import { useNavigate } from 'react-router-dom';
import { login } from '../api/authApi';
import { useAuthStore } from '../store/authStore';

export default function LoginPage() {
  const navigate = useNavigate();
  const setAuth = useAuthStore((s) => s.setAuth);
  const [form] = Form.useForm();

  const handleLogin = async (values: { email: string; password: string }) => {
    try {
      const res = await login(values.email, values.password);
      setAuth(res.token, res.adminId, res.name, res.email);
      navigate('/dashboard');
    } catch {
      message.error('Invalid email or password.');
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '100vh', background: '#f0f2f5' }}>
      <Card style={{ width: 400, boxShadow: '0 4px 20px rgba(0,0,0,0.1)' }}>
        <Typography.Title level={3} style={{ textAlign: 'center', marginBottom: 24 }}>
          Permission Management
        </Typography.Title>
        <Form form={form} layout="vertical" onFinish={handleLogin}>
          <Form.Item name="email" label="Email" rules={[{ required: true, type: 'email' }]}>
            <Input placeholder="admin@example.com" size="large" />
          </Form.Item>
          <Form.Item name="password" label="Password" rules={[{ required: true }]}>
            <Input.Password placeholder="Password" size="large" />
          </Form.Item>
          <Form.Item style={{ marginBottom: 0 }}>
            <Button type="primary" htmlType="submit" block size="large">
              Login
            </Button>
          </Form.Item>
        </Form>
        <Typography.Text type="secondary" style={{ display: 'block', marginTop: 16, fontSize: 12 }}>
          Demo accounts: super@example.com / basra@example.com / readonly@example.com (password: Admin@123)
        </Typography.Text>
      </Card>
    </div>
  );
}
