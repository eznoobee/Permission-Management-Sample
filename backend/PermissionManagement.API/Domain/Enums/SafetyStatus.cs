namespace PermissionManagement.API.Domain.Enums;

public enum SafetyStatus
{
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
    PrintingTheBook = 16
}
