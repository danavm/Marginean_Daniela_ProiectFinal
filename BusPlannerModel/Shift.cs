namespace BusPlannerModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shift")]
    public partial class Shift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shift()
        {
            Planners = new HashSet<Planner>();
        }

        [Key]
        public int IdShift { get; set; }

        [Required]
        [StringLength(50)]
        public string ShiftName { get; set; }

        public TimeSpan StartShift { get; set; }

        public TimeSpan EndShift { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Planner> Planners { get; set; }
    }
}
