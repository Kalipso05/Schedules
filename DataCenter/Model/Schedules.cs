namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Schedules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schedules()
        {
            ScheduleChanges = new HashSet<ScheduleChanges>();
            ScheduleEvents = new HashSet<ScheduleEvents>();
        }

        [Key]
        public int ScheduleID { get; set; }

        public int? UserID { get; set; }

        public bool Approved { get; set; }

        public int? ApprovedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ScheduleDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [StringLength(50)]
        public string ScheduleType { get; set; }

        [StringLength(7)]
        public string Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleChanges> ScheduleChanges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleEvents> ScheduleEvents { get; set; }

        public virtual Users Users { get; set; }
    }
}
