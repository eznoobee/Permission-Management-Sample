import { PermissionAction, SafetyStatus, ServiceType, YearlyOrderStatus } from '../types/enums';

export const serviceTypeLabels: Record<ServiceType, string> = {
  [ServiceType.YearlyPayment]: 'Yearly Payment',
  [ServiceType.MonthlyPayment]: 'Monthly Payment',
  [ServiceType.SafetyCheck]: 'Safety Check',
};

export const actionLabels: Record<PermissionAction, string> = {
  [PermissionAction.Create]: 'Create',
  [PermissionAction.Edit]: 'Edit',
  [PermissionAction.Approve]: 'Approve',
  [PermissionAction.Reject]: 'Reject',
  [PermissionAction.ChangeStatus]: 'Change Status',
  [PermissionAction.Export]: 'Export',
  [PermissionAction.Print]: 'Print',
};

export const safetyStatusLabels: Record<number, string> = {
  [SafetyStatus.Creating]: 'Creating',
  [SafetyStatus.ProcessAndReview]: 'Process & Review',
  [SafetyStatus.Canceled]: 'Canceled',
  [SafetyStatus.OnProcessing]: 'On Processing',
  [SafetyStatus.Rejected]: 'Rejected',
  [SafetyStatus.AdditionalProcedures]: 'Additional Procedures',
  [SafetyStatus.ReviewAdditionalProcedures]: 'Review Additional Procedures',
  [SafetyStatus.NeedToUpdate]: 'Need To Update',
  [SafetyStatus.OnReview]: 'On Review',
  [SafetyStatus.NeedToPay]: 'Need To Pay',
  [SafetyStatus.Paid]: 'Paid',
  [SafetyStatus.PaidConfirmed]: 'Paid Confirmed',
  [SafetyStatus.Completed]: 'Completed',
  [SafetyStatus.RequiresPromise]: 'Requires Promise',
  [SafetyStatus.PrintPaymentInformation]: 'Print Payment Information',
  [SafetyStatus.PrintingTheBook]: 'Printing The Book',
};

export const yearlyStatusLabels: Record<number, string> = {
  [YearlyOrderStatus.Canceled]: 'Canceled',
  [YearlyOrderStatus.Creating]: 'Creating',
  [YearlyOrderStatus.Reviewing]: 'Reviewing',
  [YearlyOrderStatus.NeedToPay]: 'Need To Pay',
  [YearlyOrderStatus.NeedPaymentConformation]: 'Need Payment Confirmation',
  [YearlyOrderStatus.FinalReview]: 'Final Review',
  [YearlyOrderStatus.Completed]: 'Completed',
  [YearlyOrderStatus.NeedAdditionalInformation]: 'Need Additional Information',
  [YearlyOrderStatus.Processing]: 'Processing',
  [YearlyOrderStatus.NeedToUpdate]: 'Need To Update',
  [YearlyOrderStatus.NeedAdvancedPaymentOrderConformation]: 'Need Advanced Payment Confirmation',
  [YearlyOrderStatus.NeedAdvancedPaymentOrderUpdate]: 'Need Advanced Payment Update',
  [YearlyOrderStatus.NeedToPayAfterPostInquiry]: 'Need To Pay After Post Inquiry',
};

export const monthlyStatusLabels = yearlyStatusLabels;

export function getStatusLabels(serviceType: ServiceType): Record<number, string> {
  switch (serviceType) {
    case ServiceType.SafetyCheck: return safetyStatusLabels;
    case ServiceType.YearlyPayment: return yearlyStatusLabels;
    case ServiceType.MonthlyPayment: return monthlyStatusLabels;
  }
}

export function getStatusOptions(serviceType: ServiceType) {
  return Object.entries(getStatusLabels(serviceType)).map(([value, label]) => ({
    value: Number(value),
    label,
  }));
}
