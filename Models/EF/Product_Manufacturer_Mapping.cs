namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Manufacturer_Mapping
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ManufacturerId { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Product Product { get; set; }
    }
}
