import { Card, Col, Row, Typography, Statistic } from 'antd';
import { useQuery } from '@tanstack/react-query';
import { getAdmins } from '../api/adminsApi';
import { getTemplates } from '../api/templatesApi';

export default function DashboardPage() {
  const { data: admins } = useQuery({ queryKey: ['admins'], queryFn: getAdmins });
  const { data: templates } = useQuery({ queryKey: ['templates'], queryFn: getTemplates });

  return (
    <div>
      <Typography.Title level={3}>Dashboard</Typography.Title>
      <Row gutter={16}>
        <Col span={6}>
          <Card>
            <Statistic title="Total Admins" value={admins?.length ?? 0} />
          </Card>
        </Col>
        <Col span={6}>
          <Card>
            <Statistic title="Active Admins" value={admins?.filter((a) => a.isActive).length ?? 0} />
          </Card>
        </Col>
        <Col span={6}>
          <Card>
            <Statistic title="Permission Templates" value={templates?.length ?? 0} />
          </Card>
        </Col>
        <Col span={6}>
          <Card>
            <Statistic title="Active Templates" value={templates?.filter((t) => t.isActive).length ?? 0} />
          </Card>
        </Col>
      </Row>
    </div>
  );
}
