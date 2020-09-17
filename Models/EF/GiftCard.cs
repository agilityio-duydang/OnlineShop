namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiftCard")]
    public partial class GiftCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GiftCard()
        {
            GiftCardUsageHistories = new HashSet<GiftCardUsageHistory>();
        }

        public int Id { get; set; }

        public int? PurchasedWithOrderItemId { get; set; }

        public int GiftCardTypeId { get; set; }

        public decimal Amount { get; set; }

        public bool IsGiftCardActivated { get; set; }

        public string GiftCardCouponCode { get; set; }

        public string RecipientName { get; set; }

        public string RecipientEmail { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string Message { get; set; }

        public bool IsRecipientNotified { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual OrderItem OrderItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiftCardUsageHistory> GiftCardUsageHistories { get; set; }
    }
}
