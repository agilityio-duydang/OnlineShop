namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorNote")]
    public partial class VendorNote
    {
        public int Id { get; set; }

        public int VendorId { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
