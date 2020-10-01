namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductsGroup")]
    public partial class ProductsGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductsGroup()
        {
            ProductsGroupItems = new HashSet<ProductsGroupItem>();
        }

        public int Id { get; set; }

        public bool Published { get; set; }

        public string Title { get; set; }

        public int NumberOfProductsPerItem { get; set; }

        public int DisplayOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsGroupItem> ProductsGroupItems { get; set; }
    }
}
