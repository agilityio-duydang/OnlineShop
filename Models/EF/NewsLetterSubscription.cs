namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsLetterSubscription")]
    public partial class NewsLetterSubscription
    {
        public int Id { get; set; }

        public Guid NewsLetterSubscriptionGuid { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
