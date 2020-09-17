namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerRole")]
    public partial class CustomerRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerRole()
        {
            Customers = new HashSet<Customer>();
            PermissionRecords = new HashSet<PermissionRecord>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool FreeShipping { get; set; }

        public bool Active { get; set; }

        public bool IsSystemRole { get; set; }

        [StringLength(255)]
        public string SystemName { get; set; }

        public bool EnablePasswordLifetime { get; set; }

        public int PurchasedWithProductId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PermissionRecord> PermissionRecords { get; set; }
    }
}
