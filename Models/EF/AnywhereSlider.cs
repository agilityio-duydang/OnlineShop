namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnywhereSlider")]
    public partial class AnywhereSlider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnywhereSlider()
        {
            SliderImages = new HashSet<SliderImage>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string SystemName { get; set; }

        public int SliderType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SliderImage> SliderImages { get; set; }
    }
}
