namespace PermissionManagement.API.Domain.Enums;

public enum MonthlyOrderStatus
{
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
    NeedToPayAfterPostInquiry = 51
}
