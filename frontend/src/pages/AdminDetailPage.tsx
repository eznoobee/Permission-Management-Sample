import { Button, Card, Col, Form, Row, Select, Space, Tabs, Tag, Typography, message } from 'antd';
import { ArrowLeftOutlined, SaveOutlined, ThunderboltOutlined } from '@ant-design/icons';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { useNavigate, useParams } from 'react-router-dom';
import { applyTemplate, getAdminPermissions, updateAdminPermissions } from '../api/adminsApi';
import { getTemplates } from '../api/templatesApi';
import { checkPermission } from '../api/permissionsApi';
import PermissionEntryForm from '../components/permissions/PermissionEntryForm';
import { PermissionAction, ServiceType } from '../types/enums';
import { actionLabels, serviceTypeLabels } from '../utils/enumLabels';
import { useState } from 'react';

const serviceOptions = Object.values(ServiceType)
  .filter((v) => typeof v === 'number')
  .map((v) => ({ value: v, label: serviceTypeLabels[v as ServiceType] }));

const actionOptions = Object.values(PermissionAction)
  .filter((v) => typeof v === 'number')
  .map((v) => ({ value: v, label: actionLabels[v as PermissionAction] }));

export default function AdminDetailPage() {
  const { id } = useParams<{ id: string }>();
  const adminId = Number(id);
  const navigate = useNavigate();
  const qc = useQueryClient();
  const [permForm] = Form.useForm();
  const [checkForm] = Form.useForm();
  const [checkResult, setCheckResult] = useState<boolean | null>(null);

  const { data: admin, isLoading } = useQuery({
    queryKey: ['admin', adminId],
    queryFn: () => getAdminPermissions(adminId),
  });

  const { data: templates = [] } = useQuery({ queryKey: ['templates'], queryFn: getTemplates });

  const saveMutation = useMutation({
    mutationFn: (values: any) =>
      updateAdminPermissions(adminId, values.viewPermissions ?? [], values.actionPermissions ?? []),
    onSuccess: () => { qc.invalidateQueries({ queryKey: ['admin', adminId] }); message.success('Permissions saved.'); },
  });

  const applyMutation = useMutation({
    mutationFn: (templateId: number) => applyTemplate(adminId, templateId),
    onSuccess: (data) => {
      qc.setQueryData(['admin', adminId], data);
      permForm.setFieldsValue({ viewPermissions: data.viewPermissions, actionPermissions: data.actionPermissions });
      message.success('Template applied — entries added to admin permissions.');
    },
  });

  const handleCheck = async (values: any) => {
    const res = await checkPermission({
      adminId,
      serviceType: values.serviceType,
      checkType: values.checkType,
      action: values.action,
      governorateId: values.governorateId,
      status: values.status,
      target: values.target,
    });
    setCheckResult(res.allowed);
  };

  if (isLoading || !admin) return null;

  const initialValues = {
    viewPermissions: admin.viewPermissions,
    actionPermissions: admin.actionPermissions,
  };

  return (
    <div>
      <Button icon={<ArrowLeftOutlined />} onClick={() => navigate('/admins')} style={{ marginBottom: 16 }}>
        Back to Admins
      </Button>
      <Typography.Title level={3} style={{ marginBottom: 4 }}>{admin.name}</Typography.Title>
      <Typography.Text type="secondary">{admin.email}</Typography.Text>

      <Tabs
        style={{ marginTop: 16 }}
        items={[
          {
            key: 'permissions',
            label: 'Permissions',
            children: (
              <Form form={permForm} initialValues={initialValues} onFinish={(v) => saveMutation.mutate(v)} layout="vertical">
                <Card
                  title="Apply Template"
                  size="small"
                  style={{ marginBottom: 16 }}
                  extra={
                    <Select
                      style={{ width: 280 }}
                      placeholder="Select a template to apply..."
                      options={templates.map((t) => ({ value: t.id, label: t.name }))}
                      onChange={(templateId) => applyMutation.mutate(templateId)}
                    />
                  }
                >
                  <Typography.Text type="secondary">
                    Applying a template appends its permission entries to the admin's current permissions.
                  </Typography.Text>
                </Card>

                <Card title="View Permissions" size="small" style={{ marginBottom: 16 }}>
                  <PermissionEntryForm name="viewPermissions" type="view" />
                </Card>

                <Card title="Action Permissions" size="small" style={{ marginBottom: 16 }}>
                  <PermissionEntryForm name="actionPermissions" type="action" />
                </Card>

                <Button type="primary" icon={<SaveOutlined />} htmlType="submit" loading={saveMutation.isPending}>
                  Save Permissions
                </Button>
              </Form>
            ),
          },
          {
            key: 'checker',
            label: 'Permission Checker',
            children: (
              <Card>
                <Typography.Title level={5}>Test Permission</Typography.Title>
                <Form form={checkForm} layout="vertical" onFinish={handleCheck}>
                  <Row gutter={16}>
                    <Col span={6}>
                      <Form.Item name="checkType" label="Check Type" rules={[{ required: true }]}>
                        <Select options={[{ value: 'view', label: 'View' }, { value: 'action', label: 'Action' }]} />
                      </Form.Item>
                    </Col>
                    <Col span={6}>
                      <Form.Item name="serviceType" label="Service Type" rules={[{ required: true }]}>
                        <Select options={serviceOptions} />
                      </Form.Item>
                    </Col>
                    <Col span={6}>
                      <Form.Item name="action" label="Action (for action check)">
                        <Select options={actionOptions} allowClear />
                      </Form.Item>
                    </Col>
                    <Col span={6}>
                      <Form.Item name="governorateId" label="Governorate ID">
                        <Select options={Array.from({ length: 18 }, (_, i) => ({ value: i + 1, label: `Governorate ${i + 1}` }))} allowClear />
                      </Form.Item>
                    </Col>
                  </Row>
                  <Button type="primary" htmlType="submit" icon={<ThunderboltOutlined />}>Check</Button>
                  {checkResult !== null && (
                    <Tag color={checkResult ? 'success' : 'error'} style={{ marginLeft: 16, fontSize: 14, padding: '4px 12px' }}>
                      {checkResult ? 'ALLOWED' : 'DENIED'}
                    </Tag>
                  )}
                </Form>
              </Card>
            ),
          },
          {
            key: 'summary',
            label: 'Effective Permissions',
            children: (
              <Card>
                <Typography.Title level={5}>View Permissions ({admin.viewPermissions.length})</Typography.Title>
                {admin.viewPermissions.map((vp, i) => (
                  <Card key={i} size="small" style={{ marginBottom: 8 }}>
                    <Space wrap>
                      <Tag color="blue">{serviceTypeLabels[vp.serviceType]}</Tag>
                      <Tag>{vp.allowedStatuses ? `${vp.allowedStatuses.length} statuses` : 'All statuses'}</Tag>
                      <Tag>{vp.allowedGovernorates ? `${vp.allowedGovernorates.length} governorates` : 'All governorates'}</Tag>
                      {vp.allowedTargets && <Tag color="orange">Targets: {vp.allowedTargets.join(', ')}</Tag>}
                    </Space>
                  </Card>
                ))}
                <Typography.Title level={5} style={{ marginTop: 16 }}>Action Permissions ({admin.actionPermissions.length})</Typography.Title>
                {admin.actionPermissions.map((ap, i) => (
                  <Card key={i} size="small" style={{ marginBottom: 8 }}>
                    <Space wrap>
                      <Tag color="green">{serviceTypeLabels[ap.serviceType]}</Tag>
                      {ap.actions.map((a) => <Tag key={a} color="geekblue">{actionLabels[a]}</Tag>)}
                      <Tag>{ap.allowedGovernorates ? `${ap.allowedGovernorates.length} governorates` : 'All governorates'}</Tag>
                      {ap.allowedTargets && <Tag color="orange">Targets: {ap.allowedTargets.join(', ')}</Tag>}
                    </Space>
                  </Card>
                ))}
              </Card>
            ),
          },
        ]}
      />
    </div>
  );
}
