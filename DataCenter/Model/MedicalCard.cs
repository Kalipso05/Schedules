namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicalCard")]
    public partial class MedicalCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalCard()
        {
            Patient = new HashSet<Patient>();
            Prescription = new HashSet<Prescription>();
            Referrals = new HashSet<Referrals>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfIssue { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfLastApeal { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfNextApeal { get; set; }

        [StringLength(50)]
        public string IdentificationCode { get; set; }

        [StringLength(50)]
        public string PathQRCode { get; set; }

        public string Photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescription> Prescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Referrals> Referrals { get; set; }
    }
}
