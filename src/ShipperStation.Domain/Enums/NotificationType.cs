using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum NotificationType
{

    [Description("OTP created")]
    VerificationCode,

    [Description("[System] New staff account created")]
    SystemStaffCreated,

    //[Description("[System] Locker connected to the system")]
    //SystemLockerConnected,

    //[Description("[System] Locker disconnected to the system")]
    //SystemLockerDisconnected,

    //[Description("[System] Locker is going to be overloaded")]
    //SystemLockerBoxWarning,

    //[Description("[System] Locker is overloaded")]
    //SystemLockerBoxOverloaded,

    //[Description("[System] New order created")]
    //SystemOrderCreated,

    //[Description("[System] Delivery is overtime")]
    //SystemOrderOverTime,

    [Description("[Customer] You has a new package")]
    CustomerPackageCreatedReceiverSms,

    [Description("[Customer] You has a new a package")]
    CustomerPackageCreatedReceiverApp,

    [Description("[Customer] You have sent a package")]
    CustomerPackageCreatedSenderSms,

    [Description("[Customer] You have sent a package")]
    CustomerPackageCreatedSenderApp,

    //[Description("[Customer] Your package is returned to the sender")]
    //CustomerPackageReturned,

    //[Description("[Customer] Your package is canceled")]
    //CustomerPackageCanceled,

    //[Description("[Customer] Your package is completed")]
    //CustomerPackageCompleted,
}

