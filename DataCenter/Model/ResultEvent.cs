namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResultEvent")]
    public partial class ResultEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResultEvent()
        {
            TherapeuticDiagnosticMeasures = new HashSet<TherapeuticDiagnosticMeasures>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ValueAnalyses { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string PrescribedMedications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TherapeuticDiagnosticMeasures> TherapeuticDiagnosticMeasures { get; set; }
    }
}
