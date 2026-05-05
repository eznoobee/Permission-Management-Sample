import { Card, Table, Tag, Typography } from 'antd';
import { useQuery } from '@tanstack/react-query';
import { getMonthlyOrders } from '../../api/ordersApi';
import { monthlyStatusLabels } from '../../utils/enumLabels';
import type { MonthlyOrder } from '../../types/orders';

export default function MonthlyOrdersPage() {
  const { data: orders = [], isLoading } = useQuery({ queryKey: ['orders', 'monthly'], queryFn: getMonthlyOrders });

  const columns = [
    { title: '#', dataIndex: 'id', key: 'id', width: 60 },
    { title: 'Applicant', dataIndex: 'applicantName', key: 'name' },
    {
      title: 'Status',
      dataIndex: 'status',
      key: 'status',
      render: (s: number) => <Tag color="purple">{monthlyStatusLabels[s] ?? s}</Tag>,
    },
    { title: 'Governorate', dataIndex: 'governorateNameEnglish', key: 'gov' },
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
      <Typography.Title level={3}>Monthly Payment Orders</Typography.Title>
      <Typography.Text type="secondary" style={{ display: 'block', marginBottom: 16 }}>
        Showing {orders.length} orders visible to your account based on your view permissions.
      </Typography.Text>
      <Card>
        <Table<MonthlyOrder> dataSource={orders} columns={columns} rowKey="id" loading={isLoading} />
      </Card>
    </div>
  );
}
