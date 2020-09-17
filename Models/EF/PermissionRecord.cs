namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionRecord")]
    public partial class PermissionRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PermissionRecord()
        {
            CustomerRoles = new HashSet<CustomerRole>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string SystemName { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRole> CustomerRoles { get; set; }
    }
}
