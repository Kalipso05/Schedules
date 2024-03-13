namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bed")]
    public partial class Bed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bed()
        {
            Hospitalization = new HashSet<Hospitalization>();
        }

        public int ID { get; set; }

        public int WardID { get; set; }

        public int Number { get; set; }

        public bool IsAvailable { get; set; }

        public virtual Ward Ward { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospitalization> Hospitalization { get; set; }
    }
}
