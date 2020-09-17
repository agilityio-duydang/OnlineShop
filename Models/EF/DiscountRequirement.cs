namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountRequirement")]
    public partial class DiscountRequirement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiscountRequirement()
        {
            DiscountRequirement1 = new HashSet<DiscountRequirement>();
        }

        public int Id { get; set; }

        public int DiscountId { get; set; }

        public string DiscountRequirementRuleSystemName { get; set; }

        public int? ParentId { get; set; }

        public int? InteractionTypeId { get; set; }

        public bool IsGroup { get; set; }

        public virtual Discount Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountRequirement> DiscountRequirement1 { get; set; }

        public virtual DiscountRequirement DiscountRequirement2 { get; set; }
    }
}
