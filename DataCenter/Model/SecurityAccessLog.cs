namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityAccessLog")]
    public partial class SecurityAccessLog
    {
        public int Id { get; set; }

        public int PersonCode { get; set; }

        [StringLength(50)]
        public string PersonRole { get; set; }

        public int? LastSecurityPointNumber { get; set; }

        [StringLength(10)]
        public string LastSecurityPointDirection { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastSecurityPointTime { get; set; }
    }
}
