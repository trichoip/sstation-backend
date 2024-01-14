using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum NotificationType
{
    /**
     * COMMON NOTIFICATION TYPES
     */
    [Description("OTP created")]
    VerificationCode,

    /**
     * SYSTEM NOTIFICATION TYPES
     */
    // Account
    [Description("[System] New staff account created")]
    SystemStaffCreated,

    // Locker
    [Description("[System] Locker connected to the system")]
    SystemLockerConnected,

    [Description("[System] Locker disconnected to the system")]
    SystemLockerDisconnected,

    [Description("[System] Locker is going to be overloaded")]
    SystemLockerBoxWarning,

    [Description("[System] Locker is overloaded")]
    SystemLockerBoxOverloaded,

    // Delivery
    [Description("[System] New order created")]
    SystemOrderCreated,

    [Description("[System] Delivery is overtime")]
    SystemOrderOverTime,

    /**
     * CUSTOMER NOTIFICATION TYPES
     */

    // Delivery
    [Description("[Customer] You has a new order created")]
    CustomerOrderCreated,

    [Description("[Customer] Your order is returned to the locker")]
    CustomerOrderReturned,

    [Description("[Customer] Your order is canceled")]
    CustomerOrderCanceled,

    [Description("[Customer] Your order is completed")]
    CustomerOrderCompleted,

    [Description("[Customer] Your Delivery is overtime")]
    CustomerOrderOverTime,

}

