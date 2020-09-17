namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PollVotingRecord")]
    public partial class PollVotingRecord
    {
        public int Id { get; set; }

        public int PollAnswerId { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PollAnswer PollAnswer { get; set; }
    }
}
