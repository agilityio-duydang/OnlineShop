namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SaleOfTheDayOffer_Product_Mapping
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SaleOfTheDayOfferId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Product Product { get; set; }

        public virtual SaleOfTheDayOffer SaleOfTheDayOffer { get; set; }
    }
}
