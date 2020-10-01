namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JCarousel")]
    public partial class JCarousel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JCarousel()
        {
            JCarousel_Product_Mapping = new HashSet<JCarousel_Product_Mapping>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public string DataSourceType { get; set; }

        public int CarouselType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JCarousel_Product_Mapping> JCarousel_Product_Mapping { get; set; }
    }
}
