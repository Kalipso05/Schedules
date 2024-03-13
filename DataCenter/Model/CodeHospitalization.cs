namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CodeHospitalization")]
    public partial class CodeHospitalization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CodeHospitalization()
        {
            Hospitalization = new HashSet<Hospitalization>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string PurposeHospitalization { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartmentHospitalization { get; set; }

        public int IDTypeHospitalization { get; set; }

        public int TermHospitalization { get; set; }

        [Required]
        [StringLength(150)]
        public string AdditionalInformation { get; set; }

        public virtual TypeHospitalization TypeHospitalization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospitalization> Hospitalization { get; set; }
    }
}
