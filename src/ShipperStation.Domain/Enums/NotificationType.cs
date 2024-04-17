using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum NotificationType
{

    [Description("OTP created")]
    VerificationCode,

    [Description("New staff account created")]
    SystemStaffCreated,

    [Description("You has a new a package")]
    CustomerPackageCreated,

    [Description("The package you sent has been paid")]
    CustomerPaymentPackage,

    [Description("Your have package is canceled")]
    CustomerPackageCanceled,

    [Description("Your have package is returned")]
    CustomerPackageReturned,

    [Description("Your have package is completed")]
    CustomerPackageCompleted,

    [Description("Your have package is exprired")]
    PackageExprire,

    [Description("You need receive package")]
    PackageReceive,
}

