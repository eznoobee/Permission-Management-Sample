export enum ServiceType {
  YearlyPayment = 1,
  MonthlyPayment = 2,
  SafetyCheck = 3,
}

export enum PermissionAction {
  Create = 2,
  Edit = 3,
  Approve = 4,
  Reject = 5,
  ChangeStatus = 6,
  Export = 7,
  Print = 8,
}

export enum SafetyStatus {
  Creating = 1,
  ProcessAndReview = 2,
  Canceled = 3,
  OnProcessing = 4,
  Rejected = 5,
  AdditionalProcedures = 6,
  ReviewAdditionalProcedures = 7,
  NeedToUpdate = 8,
  OnReview = 9,
  NeedToPay = 10,
  Paid = 11,
  PaidConfirmed = 12,
  Completed = 13,
  RequiresPromise = 14,
  PrintPaymentInformation = 15,
  PrintingTheBook = 16,
}

export enum YearlyOrderStatus {
  Canceled = 0,
  Creating = 1,
  Reviewing = 4,
  NeedToPay = 5,
  NeedPaymentConformation = 6,
  FinalReview = 7,
  Completed = 8,
  NeedAdditionalInformation = 30,
  Processing = 31,
  NeedToUpdate = 32,
  NeedAdvancedPaymentOrderConformation = 21,
  NeedAdvancedPaymentOrderUpdate = 22,
  NeedToPayAfterPostInquiry = 51,
}

export enum MonthlyOrderStatus {
  Canceled = 0,
  Creating = 1,
  Reviewing = 4,
  NeedToPay = 5,
  NeedPaymentConformation = 6,
  FinalReview = 7,
  Completed = 8,
  NeedAdditionalInformation = 30,
  Processing = 31,
  NeedToUpdate = 32,
  NeedAdvancedPaymentOrderConformation = 21,
  NeedAdvancedPaymentOrderUpdate = 22,
  NeedToPayAfterPostInquiry = 51,
}
