namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Referrals
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int IDMedicalCard { get; set; }

        public int IDUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Anamnesis { get; set; }

        [Required]
        public string Symptoms { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Recommendations { get; set; }

        public virtual MedicalCard MedicalCard { get; set; }

        public virtual Users Users { get; set; }
    }
}
