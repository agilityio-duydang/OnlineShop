namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductsGroupItem")]
    public partial class ProductsGroupItem
    {
        public ProductsGroupItem()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public int SourceType { get; set; }

        public int CategoryId { get; set; }

        public int SortMethod { get; set; }

        public int DisplayOrder { get; set; }

        public int GroupId { get; set; }

        public virtual ProductsGroup ProductsGroup { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
