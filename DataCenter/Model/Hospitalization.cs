namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hospitalization")]
    public partial class Hospitalization
    {
        public int ID { get; set; }

        public int IDPatient { get; set; }

        public int IDCodeHospitalization { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfHospitalization { get; set; }

        public TimeSpan TimeOfHospitalization { get; set; }

        public bool? StatusHospitalization { get; set; }

        [StringLength(150)]
        public string ReasonRejection { get; set; }

        public int? WardID { get; set; }

        public int? BedID { get; set; }

        public virtual Bed Bed { get; set; }

        public virtual CodeHospitalization CodeHospitalization { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
