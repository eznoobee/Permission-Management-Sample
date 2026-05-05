import { PermissionAction, ServiceType } from './enums';

export interface ViewPermissionEntry {
  serviceType: ServiceType;
  allowedStatuses: number[] | null;
  allowedGovernorates: number[] | null;
  allowedTargets: string[] | null;
}

export interface ActionPermissionEntry {
  serviceType: ServiceType;
  actions: PermissionAction[];
  allowedGovernorates: number[] | null;
  allowedTargets: string[] | null;
}

export interface AdminPermissionsDto {
  adminId: number;
  name: string;
  email: string;
  isActive: boolean;
  viewPermissions: ViewPermissionEntry[];
  actionPermissions: ActionPermissionEntry[];
}

export interface PermissionTemplate {
  id: number;
  name: string;
  description: string | null;
  isActive: boolean;
  viewPermissions: ViewPermissionEntry[];
  actionPermissions: ActionPermissionEntry[];
}
