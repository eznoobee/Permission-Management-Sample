import { MonthlyOrderStatus, SafetyStatus, YearlyOrderStatus } from './enums';

export interface YearlyOrder {
  id: number;
  applicantName: string;
  status: YearlyOrderStatus;
  governorateId: number;
  governorateNameEnglish: string;
  createdAt: string;
  allowedActions: string[];
}

export interface MonthlyOrder {
  id: number;
  applicantName: string;
  status: MonthlyOrderStatus;
  governorateId: number;
  governorateNameEnglish: string;
  createdAt: string;
  allowedActions: string[];
}

export interface SafetyOrder {
  id: number;
  applicantName: string;
  status: SafetyStatus;
  governorateId: number;
  governorateNameEnglish: string;
  target: string;
  createdAt: string;
  allowedActions: string[];
}
