namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Category_Mapping
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
