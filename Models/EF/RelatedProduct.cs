namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelatedProduct")]
    public partial class RelatedProduct
    {
        public int Id { get; set; }

        public int ProductId1 { get; set; }

        public int ProductId2 { get; set; }

        public int DisplayOrder { get; set; }
    }
}
