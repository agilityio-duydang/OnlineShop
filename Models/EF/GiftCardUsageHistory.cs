namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiftCardUsageHistory")]
    public partial class GiftCardUsageHistory
    {
        public int Id { get; set; }

        public int GiftCardId { get; set; }

        public int UsedWithOrderId { get; set; }

        public decimal UsedValue { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual GiftCard GiftCard { get; set; }

        public virtual Order Order { get; set; }
    }
}
