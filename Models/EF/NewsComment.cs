namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsComment")]
    public partial class NewsComment
    {
        public int Id { get; set; }

        public string CommentTitle { get; set; }

        public string CommentText { get; set; }

        public int NewsItemId { get; set; }

        public int CustomerId { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual News News { get; set; }
    }
}
