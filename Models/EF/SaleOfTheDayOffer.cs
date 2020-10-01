namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SaleOfTheDayOffer")]
    public partial class SaleOfTheDayOffer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleOfTheDayOffer()
        {
            SaleOfTheDayOffer_Product_Mapping = new HashSet<SaleOfTheDayOffer_Product_Mapping>();
        }

        public int Id { get; set; }

        public bool Published { get; set; }

        public string Title { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int SchedulePatternType { get; set; }

        public TimeSpan? SchedulePatternFromTime { get; set; }

        public TimeSpan? SchedulePatternToTime { get; set; }

        public int? ExactDayValue { get; set; }

        public int? EveryMonthFromDayValue { get; set; }

        public int? EveryMonthToDayValue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOfTheDayOffer_Product_Mapping> SaleOfTheDayOffer_Product_Mapping { get; set; }
    }
}
