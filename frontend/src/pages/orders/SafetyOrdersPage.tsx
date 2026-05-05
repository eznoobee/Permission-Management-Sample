import { Card, Table, Tag, Typography } from 'antd';
import { useQuery } from '@tanstack/react-query';
import { getSafetyOrders } from '../../api/ordersApi';
import { safetyStatusLabels } from '../../utils/enumLabels';
import type { SafetyOrder } from '../../types/orders';

export default function SafetyOrdersPage() {
  const { data: orders = [], isLoading } = useQuery({ queryKey: ['orders', 'safety'], queryFn: getSafetyOrders });

  const columns = [
    { title: '#', dataIndex: 'id', key: 'id', width: 60 },
    { title: 'Applicant', dataIndex: 'applicantName', key: 'name' },
    {
      title: 'Status',
      dataIndex: 'status',
      key: 'status',
      render: (s: number) => <Tag color="warning">{safetyStatusLabels[s] ?? s}</Tag>,
    },
    { title: 'Governorate', dataIndex: 'governorateNameEnglish', key: 'gov' },
    { title: 'Target', dataIndex: 'target', key: 'target', render: (t: string) => <Tag color="orange">{t}</Tag> },
    {
      title: 'Allowed Actions',
      dataIndex: 'allowedActions',
      key: 'actions',
      render: (actions: string[]) =>
        actions.length === 0
          ? <Tag>View only</Tag>
          : actions.map((a) => <Tag key={a} color="green">{a}</Tag>),
    },
  ];

  return (
    <div>
      <Typography.Title level={3}>Safety Check Orders</Typography.Title>
      <Typography.Text type="secondary" style={{ display: 'block', marginBottom: 16 }}>
        Showing {orders.length} orders visible to your account, filtered by governorate and target permissions.
      </Typography.Text>
      <Card>
        <Table<SafetyOrder> dataSource={orders} columns={columns} rowKey="id" loading={isLoading} />
      </Card>
    </div>
  );
}
