namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SliderImage")]
    public partial class SliderImage
    {
        public int Id { get; set; }

        public string DisplayText { get; set; }

        public string Url { get; set; }

        public string Alt { get; set; }

        public bool Visible { get; set; }

        public int DisplayOrder { get; set; }

        public int PictureId { get; set; }

        public int SliderId { get; set; }

        public virtual AnywhereSlider AnywhereSlider { get; set; }
    }
}
