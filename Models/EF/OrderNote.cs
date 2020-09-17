namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderNote")]
    public partial class OrderNote
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Required]
        public string Note { get; set; }

        public bool DisplayToCustomer { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Order Order { get; set; }
    }
}
