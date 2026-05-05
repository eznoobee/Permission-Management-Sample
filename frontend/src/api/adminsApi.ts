import api from './axiosInstance';
import type { AdminPermissionsDto, ActionPermissionEntry, ViewPermissionEntry } from '../types/permissions';

export const getAdmins = (): Promise<AdminPermissionsDto[]> =>
  api.get('/admins').then((r) => r.data);

export const getAdmin = (id: number): Promise<AdminPermissionsDto> =>
  api.get(`/admins/${id}`).then((r) => r.data);

export const getAdminPermissions = (id: number): Promise<AdminPermissionsDto> =>
  api.get(`/admins/${id}/permissions`).then((r) => r.data);

export const updateAdminPermissions = (
  id: number,
  viewPermissions: ViewPermissionEntry[],
  actionPermissions: ActionPermissionEntry[]
): Promise<AdminPermissionsDto> =>
  api.put(`/admins/${id}/permissions`, { viewPermissions, actionPermissions }).then((r) => r.data);

export const applyTemplate = (adminId: number, templateId: number): Promise<AdminPermissionsDto> =>
  api.post(`/admins/${adminId}/apply-template/${templateId}`).then((r) => r.data);
