namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleTemplates
    {
        [Key]
        public int TemplateID { get; set; }

        public int? SpecialityID { get; set; }

        public int? DayOfWeek { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [StringLength(50)]
        public string Pattern { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
