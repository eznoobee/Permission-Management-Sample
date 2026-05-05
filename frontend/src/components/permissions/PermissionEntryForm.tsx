import { Button, Col, Form, Row, Select } from 'antd';
import { PlusOutlined, DeleteOutlined } from '@ant-design/icons';
import { useQuery } from '@tanstack/react-query';
import { getGovernorates } from '../../api/governoratesApi';
import { PermissionAction, ServiceType } from '../../types/enums';
import { actionLabels, getStatusOptions, serviceTypeLabels } from '../../utils/enumLabels';

const serviceOptions = Object.values(ServiceType)
  .filter((v) => typeof v === 'number')
  .map((v) => ({ value: v, label: serviceTypeLabels[v as ServiceType] }));

const actionOptions = Object.values(PermissionAction)
  .filter((v) => typeof v === 'number')
  .map((v) => ({ value: v, label: actionLabels[v as PermissionAction] }));

const SAFETY_TARGETS = ['مواطنين', 'اجانب', 'مزدوجو الجنسية'];

interface Props {
  name: string;
  type: 'view' | 'action';
}

export default function PermissionEntryForm({ name, type }: Props) {
  const { data: governorates = [] } = useQuery({ queryKey: ['governorates'], queryFn: getGovernorates });

  const govOptions = governorates.map((g) => ({ value: g.id, label: `${g.nameEnglish} (${g.nameArabic})` }));
  const targetOptions = SAFETY_TARGETS.map((t) => ({ value: t, label: t }));

  return (
    <Form.List name={name}>
      {(fields, { add, remove }) => (
        <>
          {fields.map(({ key, name: fieldName, ...rest }) => (
            <Form.Item key={key} style={{ marginBottom: 8 }}>
              <div style={{ border: '1px solid #d9d9d9', borderRadius: 8, padding: 16, background: '#fafafa' }}>
                <Row gutter={12} align="middle">
                  <Col span={6}>
                    <Form.Item {...rest} name={[fieldName, 'serviceType']} label="Service" rules={[{ required: true }]} style={{ marginBottom: 0 }}>
                      <Select options={serviceOptions} placeholder="Service type" />
                    </Form.Item>
                  </Col>
                  {type === 'action' && (
                    <Col span={8}>
                      <Form.Item {...rest} name={[fieldName, 'actions']} label="Actions" rules={[{ required: true }]} style={{ marginBottom: 0 }}>
                        <Select mode="multiple" options={actionOptions} placeholder="Select actions" />
                      </Form.Item>
                    </Col>
                  )}
                  {type === 'view' && (
                    <Form.Item noStyle shouldUpdate>
                      {({ getFieldValue }) => {
                        const entries = getFieldValue(name) ?? [];
                        const serviceType = entries[fieldName]?.serviceType;
                        const statusOptions = serviceType ? getStatusOptions(serviceType) : [];
                        return (
                          <Col span={8}>
                            <Form.Item {...rest} name={[fieldName, 'allowedStatuses']} label="Statuses (null=all)" style={{ marginBottom: 0 }}>
                              <Select mode="multiple" options={statusOptions} placeholder="All statuses" allowClear />
                            </Form.Item>
                          </Col>
                        );
                      }}
                    </Form.Item>
                  )}
                  <Col span={type === 'view' ? 8 : 8}>
                    <Form.Item {...rest} name={[fieldName, 'allowedGovernorates']} label="Governorates (null=all)" style={{ marginBottom: 0 }}>
                      <Select mode="multiple" options={govOptions} placeholder="All governorates" allowClear />
                    </Form.Item>
                  </Col>
                </Row>
                <Row gutter={12} style={{ marginTop: 12 }}>
                  <Col span={8}>
                    <Form.Item noStyle shouldUpdate>
                      {({ getFieldValue }) => {
                        const entries = getFieldValue(name) ?? [];
                        const serviceType = entries[fieldName]?.serviceType;
                        const showTargets = serviceType === ServiceType.SafetyCheck;
                        return showTargets ? (
                          <Form.Item {...rest} name={[fieldName, 'allowedTargets']} label="Targets (null=all)" style={{ marginBottom: 0 }}>
                            <Select mode="multiple" options={targetOptions} placeholder="All targets" allowClear />
                          </Form.Item>
                        ) : null;
                      }}
                    </Form.Item>
                  </Col>
                  <Col flex="auto" style={{ display: 'flex', alignItems: 'flex-end', justifyContent: 'flex-end' }}>
                    <Button danger icon={<DeleteOutlined />} onClick={() => remove(fieldName)}>
                      Remove
                    </Button>
                  </Col>
                </Row>
              </div>
            </Form.Item>
          ))}
          <Button icon={<PlusOutlined />} onClick={() => add({ allowedStatuses: null, allowedGovernorates: null, allowedTargets: null })} block>
            Add {type === 'view' ? 'View' : 'Action'} Permission
          </Button>
        </>
      )}
    </Form.List>
  );
}
