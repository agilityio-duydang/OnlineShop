namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogComment")]
    public partial class BlogComment
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CommentText { get; set; }

        public bool IsApproved { get; set; }

        public int BlogPostId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
