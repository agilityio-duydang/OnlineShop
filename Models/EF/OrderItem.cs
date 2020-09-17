namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderItem")]
    public partial class OrderItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderItem()
        {
            GiftCards = new HashSet<GiftCard>();
        }

        public int Id { get; set; }

        public Guid OrderItemGuid { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPriceInclTax { get; set; }

        public decimal UnitPriceExclTax { get; set; }

        public decimal PriceInclTax { get; set; }

        public decimal PriceExclTax { get; set; }

        public decimal DiscountAmountInclTax { get; set; }

        public decimal DiscountAmountExclTax { get; set; }

        public decimal OriginalProductCost { get; set; }

        public decimal? ItemWeight { get; set; }

        public DateTime? RentalStartDateUtc { get; set; }

        public DateTime? RentalEndDateUtc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiftCard> GiftCards { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
