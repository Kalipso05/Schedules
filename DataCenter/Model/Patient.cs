namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Hospitalization = new HashSet<Hospitalization>();
            ReferralsService = new HashSet<ReferralsService>();
            TherapeuticDiagnosticMeasures = new HashSet<TherapeuticDiagnosticMeasures>();
        }

        public int ID { get; set; }

        public string Photo { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        public int IDPassport { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? IDGender { get; set; }

        public string Adress { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? IDMedicalCard { get; set; }

        public int? IDInsuransePolicy { get; set; }

        public string Diagnos { get; set; }

        public int? IDDiseaseHistory { get; set; }

        public int IDInsuranseCompany { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkPlace { get; set; }

        public string PathContract { get; set; }

        public string PathPersonalData { get; set; }

        public virtual DiseaseHistory DiseaseHistory { get; set; }

        public virtual Gender Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospitalization> Hospitalization { get; set; }

        public virtual InsuranseCompany InsuranseCompany { get; set; }

        public virtual InsuransePolicy InsuransePolicy { get; set; }

        public virtual MedicalCard MedicalCard { get; set; }

        public virtual Passport Passport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReferralsService> ReferralsService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TherapeuticDiagnosticMeasures> TherapeuticDiagnosticMeasures { get; set; }
    }
}
