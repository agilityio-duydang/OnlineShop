namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueuedEmail")]
    public partial class QueuedEmail
    {
        public int Id { get; set; }

        public int PriorityId { get; set; }

        [Required]
        [StringLength(500)]
        public string From { get; set; }

        [StringLength(500)]
        public string FromName { get; set; }

        [Required]
        [StringLength(500)]
        public string To { get; set; }

        [StringLength(500)]
        public string ToName { get; set; }

        [StringLength(500)]
        public string ReplyTo { get; set; }

        [StringLength(500)]
        public string ReplyToName { get; set; }

        [StringLength(500)]
        public string CC { get; set; }

        [StringLength(500)]
        public string Bcc { get; set; }

        [StringLength(1000)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public string AttachmentFilePath { get; set; }

        public string AttachmentFileName { get; set; }

        public int AttachedDownloadId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? DontSendBeforeDateUtc { get; set; }

        public int SentTries { get; set; }

        public DateTime? SentOnUtc { get; set; }

        public int EmailAccountId { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }
    }
}
