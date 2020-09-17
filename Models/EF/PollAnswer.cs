namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PollAnswer")]
    public partial class PollAnswer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PollAnswer()
        {
            PollVotingRecords = new HashSet<PollVotingRecord>();
        }

        public int Id { get; set; }

        public int PollId { get; set; }

        [Required]
        public string Name { get; set; }

        public int NumberOfVotes { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Poll Poll { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PollVotingRecord> PollVotingRecords { get; set; }
    }
}
