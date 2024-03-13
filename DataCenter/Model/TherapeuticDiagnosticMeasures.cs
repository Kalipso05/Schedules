namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TherapeuticDiagnosticMeasures
    {
        public int ID { get; set; }

        public int IDPatient { get; set; }

        public int IDUser { get; set; }

        public int IDTypeEvent { get; set; }

        [Required]
        [StringLength(50)]
        public string NameEvent { get; set; }

        public int IDResultEvent { get; set; }

        [Required]
        public string Recomendation { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual ResultEvent ResultEvent { get; set; }

        public virtual TypeEvent TypeEvent { get; set; }

        public virtual Users Users { get; set; }
    }
}
