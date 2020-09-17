namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Poll")]
    public partial class Poll
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Poll()
        {
            PollAnswers = new HashSet<PollAnswer>();
        }

        public int Id { get; set; }

        public int LanguageId { get; set; }

        [Required]
        public string Name { get; set; }

        public string SystemKeyword { get; set; }

        public bool Published { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool AllowGuestsToVote { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public virtual Language Language { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
    }
}
