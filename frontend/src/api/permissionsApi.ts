import api from './axiosInstance';
import { PermissionAction, ServiceType } from '../types/enums';

interface CheckRequest {
  adminId: number;
  serviceType: ServiceType;
  checkType: 'view' | 'action';
  action?: PermissionAction;
  governorateId?: number;
  status?: number;
  target?: string;
}

export const checkPermission = (req: CheckRequest): Promise<{ allowed: boolean }> =>
  api.post('/permissions/check', req).then((r) => r.data);
