namespace BusPlannerModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Route
    {
        [Key]
        public int IdRoute { get; set; }

        [Required]
        [StringLength(50)]
        public string StartRoute { get; set; }

        [Required]
        [StringLength(50)]
        public string EndRoute { get; set; }
    }
}
