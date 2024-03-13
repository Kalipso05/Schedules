namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReferralsService")]
    public partial class ReferralsService
    {
        [Key]
        public int ReferralID { get; set; }

        public int? PatientID { get; set; }

        public int? IssuedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssuedDate { get; set; }

        public string Purpose { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Users Users { get; set; }
    }
}
