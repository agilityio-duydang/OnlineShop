namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("BlogPost")]
    public partial class BlogPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BlogPost()
        {
            BlogComments = new HashSet<BlogComment>();
        }

        public int Id { get; set; }

        public int LanguageId { get; set; }

        [Required]
        public string Title { get; set; }

        [AllowHtml]
        [Required]
        public string Body { get; set; }

        [AllowHtml]
        public string BodyOverview { get; set; }

        public bool AllowComments { get; set; }

        public string Tags { get; set; }

        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        [StringLength(400)]
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        [StringLength(400)]
        public string MetaTitle { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlogComment> BlogComments { get; set; }

        public virtual Language Language { get; set; }
    }
}
