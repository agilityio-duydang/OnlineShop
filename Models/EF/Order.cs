namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            DiscountUsageHistories = new HashSet<DiscountUsageHistory>();
            GiftCardUsageHistories = new HashSet<GiftCardUsageHistory>();
            OrderItems = new HashSet<OrderItem>();
            OrderNotes = new HashSet<OrderNote>();
        }

        public int Id { get; set; }

        public Guid OrderGuid { get; set; }

        public int CustomerId { get; set; }

        public int BillingAddressId { get; set; }

        public int? ShippingAddressId { get; set; }

        public int? PickupAddressId { get; set; }

        public int OrderStatusId { get; set; }

        public int ShippingStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public string PaymentMethodSystemName { get; set; }

        public string CustomerCurrencyCode { get; set; }

        public decimal CurrencyRate { get; set; }

        public int CustomerTaxDisplayTypeId { get; set; }

        public string VatNumber { get; set; }

        public decimal OrderSubtotalInclTax { get; set; }

        public decimal OrderSubtotalExclTax { get; set; }

        public decimal OrderSubTotalDiscountInclTax { get; set; }

        public decimal OrderSubTotalDiscountExclTax { get; set; }

        public decimal OrderShippingInclTax { get; set; }

        public decimal OrderShippingExclTax { get; set; }

        public decimal PaymentMethodAdditionalFeeInclTax { get; set; }

        public decimal PaymentMethodAdditionalFeeExclTax { get; set; }

        public string TaxRates { get; set; }

        public decimal OrderTax { get; set; }

        public decimal OrderDiscount { get; set; }

        public decimal OrderTotal { get; set; }

        public int CustomerLanguageId { get; set; }

        public string CustomerIp { get; set; }

        public string ShippingMethod { get; set; }

        public string ShippingRateComputationMethodSystemName { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        [Required]
        public string CustomOrderNumber { get; set; }

        public virtual Address Address { get; set; }

        public virtual Address Address1 { get; set; }

        public virtual Address Address2 { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiftCardUsageHistory> GiftCardUsageHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderNote> OrderNotes { get; set; }
    }
}
