namespace DataCenter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RolePermissions
    {
        [Key]
        public int RolePermissionID { get; set; }

        public int? RoleID { get; set; }

        public int? PermissionID { get; set; }

        public virtual Permissions Permissions { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
