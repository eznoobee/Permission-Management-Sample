import { Button, Card, Form, Input, Modal, Space, Table, Tag, Typography, message, Popconfirm } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { createTemplate, deleteTemplate, getTemplates, updateTemplate } from '../api/templatesApi';
import type { PermissionTemplate } from '../types/permissions';
import { useState } from 'react';
import PermissionEntryForm from '../components/permissions/PermissionEntryForm';
import { serviceTypeLabels } from '../utils/enumLabels';

export default function TemplatesPage() {
  const qc = useQueryClient();
  const { data: templates = [], isLoading } = useQuery({ queryKey: ['templates'], queryFn: getTemplates });
  const [modal, setModal] = useState<{ open: boolean; template?: PermissionTemplate }>({ open: false });
  const [form] = Form.useForm();

  const saveMutation = useMutation({
    mutationFn: (values: any) =>
      modal.template
        ? updateTemplate(modal.template.id, values)
        : createTemplate(values),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['templates'] });
      setModal({ open: false });
      message.success('Template saved.');
    },
  });

  const deleteMutation = useMutation({
    mutationFn: deleteTemplate,
    onSuccess: () => { qc.invalidateQueries({ queryKey: ['templates'] }); message.success('Deleted.'); },
  });

  const openCreate = () => {
    form.resetFields();
    form.setFieldsValue({ viewPermissions: [], actionPermissions: [] });
    setModal({ open: true });
  };

  const openEdit = (t: PermissionTemplate) => {
    form.setFieldsValue(t);
    setModal({ open: true, template: t });
  };

  const columns = [
    { title: 'Name', dataIndex: 'name', key: 'name', render: (n: string) => <Typography.Text strong>{n}</Typography.Text> },
    { title: 'Description', dataIndex: 'description', key: 'description' },
    {
      title: 'View Rules',
      key: 'view',
      render: (_: any, t: PermissionTemplate) =>
        t.viewPermissions.map((v, i) => (
          <Tag key={i} color="blue">{serviceTypeLabels[v.serviceType]}</Tag>
        )),
    },
    {
      title: 'Action Rules',
      key: 'action',
      render: (_: any, t: PermissionTemplate) =>
        t.actionPermissions.map((a, i) => (
          <Tag key={i} color="green">{serviceTypeLabels[a.serviceType]} ({a.actions.length} actions)</Tag>
        )),
    },
    {
      title: 'Actions',
      key: 'actions',
      render: (_: any, t: PermissionTemplate) => (
        <Space>
          <Button icon={<EditOutlined />} size="small" onClick={() => openEdit(t)}>Edit</Button>
          <Popconfirm title="Delete this template?" onConfirm={() => deleteMutation.mutate(t.id)}>
            <Button icon={<DeleteOutlined />} size="small" danger>Delete</Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <Typography.Title level={3} style={{ margin: 0 }}>Permission Templates</Typography.Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={openCreate}>New Template</Button>
      </div>
      <Table dataSource={templates} columns={columns} rowKey="id" loading={isLoading} />

      <Modal
        open={modal.open}
        title={modal.template ? 'Edit Template' : 'Create Template'}
        onCancel={() => setModal({ open: false })}
        onOk={() => form.submit()}
        width={900}
        okText="Save"
      >
        <Form form={form} layout="vertical" onFinish={(v) => saveMutation.mutate(v)}>
          <Form.Item name="name" label="Name" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Description">
            <Input.TextArea rows={2} />
          </Form.Item>
          <Typography.Text strong>View Permissions</Typography.Text>
          <Card size="small" style={{ marginTop: 8, marginBottom: 16 }}>
            <PermissionEntryForm name="viewPermissions" type="view" />
          </Card>
          <Typography.Text strong>Action Permissions</Typography.Text>
          <Card size="small" style={{ marginTop: 8 }}>
            <PermissionEntryForm name="actionPermissions" type="action" />
          </Card>
        </Form>
      </Modal>
    </div>
  );
}
