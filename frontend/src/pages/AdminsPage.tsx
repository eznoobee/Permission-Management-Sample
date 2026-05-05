import { Badge, Button, Card, Table, Tag, Typography } from 'antd';
import { EyeOutlined } from '@ant-design/icons';
import { useQuery } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import { getAdmins } from '../api/adminsApi';
import type { AdminPermissionsDto } from '../types/permissions';

export default function AdminsPage() {
  const navigate = useNavigate();
  const { data: admins = [], isLoading } = useQuery({ queryKey: ['admins'], queryFn: getAdmins });

  const columns = [
    { title: 'Name', dataIndex: 'name', key: 'name', render: (n: string) => <Typography.Text strong>{n}</Typography.Text> },
    { title: 'Email', dataIndex: 'email', key: 'email' },
    {
      title: 'Status',
      dataIndex: 'isActive',
      key: 'isActive',
      render: (active: boolean) => <Badge status={active ? 'success' : 'error'} text={active ? 'Active' : 'Inactive'} />,
    },
    {
      title: 'View Rules',
      key: 'view',
      render: (_: any, a: AdminPermissionsDto) => <Tag color="blue">{a.viewPermissions.length} rules</Tag>,
    },
    {
      title: 'Action Rules',
      key: 'action',
      render: (_: any, a: AdminPermissionsDto) => <Tag color="green">{a.actionPermissions.length} rules</Tag>,
    },
    {
      title: '',
      key: 'detail',
      render: (_: any, a: AdminPermissionsDto) => (
        <Button icon={<EyeOutlined />} size="small" onClick={() => navigate(`/admins/${a.adminId}`)}>
          Manage
        </Button>
      ),
    },
  ];

  return (
    <div>
      <Typography.Title level={3}>Admins</Typography.Title>
      <Card>
        <Table dataSource={admins} columns={columns} rowKey="adminId" loading={isLoading} />
      </Card>
    </div>
  );
}
