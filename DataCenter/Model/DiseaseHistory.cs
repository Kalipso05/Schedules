namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiseaseHistory")]
    public partial class DiseaseHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiseaseHistory()
        {
            Patient = new HashSet<Patient>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameDisease { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfDisease { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
