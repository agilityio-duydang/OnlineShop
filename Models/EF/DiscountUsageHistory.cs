namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountUsageHistory")]
    public partial class DiscountUsageHistory
    {
        public int Id { get; set; }

        public int DiscountId { get; set; }

        public int OrderId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Order Order { get; set; }
    }
}
