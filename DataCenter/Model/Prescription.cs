namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prescription")]
    public partial class Prescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int IDMedicalCard { get; set; }

        [Required]
        [StringLength(50)]
        public string Medicine { get; set; }

        [Required]
        [StringLength(20)]
        public string Dosage { get; set; }

        [Required]
        [StringLength(50)]
        public string Administration { get; set; }

        public virtual MedicalCard MedicalCard { get; set; }
    }
}
