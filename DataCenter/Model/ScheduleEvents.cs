namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleEvents
    {
        [Key]
        public int EventID { get; set; }

        public int? ScheduleID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventDate { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(7)]
        public string Color { get; set; }

        public virtual Schedules Schedules { get; set; }
    }
}
