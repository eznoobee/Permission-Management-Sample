import api from './axiosInstance';
import type { PermissionTemplate } from '../types/permissions';

export const getTemplates = (): Promise<PermissionTemplate[]> =>
  api.get('/permission-templates').then((r) => r.data);

export const getTemplate = (id: number): Promise<PermissionTemplate> =>
  api.get(`/permission-templates/${id}`).then((r) => r.data);

export const createTemplate = (data: Omit<PermissionTemplate, 'id' | 'isActive'>): Promise<PermissionTemplate> =>
  api.post('/permission-templates', data).then((r) => r.data);

export const updateTemplate = (id: number, data: Omit<PermissionTemplate, 'id' | 'isActive'>): Promise<PermissionTemplate> =>
  api.put(`/permission-templates/${id}`, data).then((r) => r.data);

export const deleteTemplate = (id: number): Promise<void> =>
  api.delete(`/permission-templates/${id}`).then(() => undefined);
