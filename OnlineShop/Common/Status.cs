using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    public enum Order
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 
        /// </summary>
        Processing = 20,
        /// <summary>
        /// 
        /// </summary>
        Complete = 30,
        /// <summary>
        /// 
        /// </summary>
        Cancelled = 40,
    }
    public enum Payment
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 
        /// </summary>
        Authorized = 20,
        /// <summary>
        /// 
        /// </summary>
        Paid = 30,
        /// <summary>
        /// 
        /// </summary>
        PartiallyRefunded = 35,
        /// <summary>
        /// 
        /// </summary>
        Refunded = 40,
        /// <summary>
        /// 
        /// </summary>
        Voided = 50,
    }
    public enum Shipping
    {
        /// <summary>
        /// 
        /// </summary>
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        ShippingNotRequired = 10,
        /// <summary>
        /// 
        /// </summary>
        NotYetShipped = 20,
        /// <summary>
        /// 
        /// </summary>
        PartiallyShipped = 25,
        /// <summary>
        /// 
        /// </summary>
        Shipped = 30,
        /// <summary>
        /// 
        /// </summary>
        Delivered = 40,
    }
    public class OrderStatus
    {
        public const string Processing = "Processing";
        public const string Pending = "Pending";
        public const string Complete = "Complete";
        public const string Canceled = "Canceled";
    }
    public class PaymentStatus
    {
        public const string All = "All";
        public const string Pending = "Pending";
        public const string Authorized = "Authorized";
        public const string Paid = "Paid";
        public const string PartiallyRefunded = "Partially Refunded";
        public const string Refunded = "Refunded";
        public const string Voided = "Voided";
    }
    public class ShippingStatus
    {
        public static string All = "All";
        public static string ShippingNotRequired = "Shipping Not Required";
        public static string NotYetShipped = "Not Yet Shipped";
        public static string PartiallyShipped = "Partially Shipped";
        public static string Shipped = "Shipped";
        public static string Delivered = "Delivered";
    }
    public class PaymentMethod
    {
        public const string CheckMoneyOrder = "Check / Money Order";
        public const string CreditCard = "Credit Card";
        public const string PayPalSmartPaymentButtons = "PayPal Smart Payment Buttons";
        public const string PayPalStandard = "PayPal Standard";

    }
}