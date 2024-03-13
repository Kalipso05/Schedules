namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleChanges
    {
        [Key]
        public int ChangeID { get; set; }

        public int? ScheduleID { get; set; }

        public int? ChangedBy { get; set; }

        public DateTime? ChangeDateTime { get; set; }

        public string ChangeDescription { get; set; }

        public virtual Users Users { get; set; }

        public virtual Schedules Schedules { get; set; }
    }
}
