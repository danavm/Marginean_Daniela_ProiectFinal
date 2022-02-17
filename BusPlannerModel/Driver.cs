namespace BusPlannerModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public partial class Driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            Planners = new HashSet<Planner>();
        }

        [Key]
        public int IdDriver { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("LastName ")]
        [Required]
        [StringLength(50)]
        public string LastName_ { get; set; }

        [Required]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Planner> Planners { get; set; }
    }
}
