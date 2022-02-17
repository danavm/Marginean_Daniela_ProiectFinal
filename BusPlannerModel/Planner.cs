namespace BusPlannerModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Planner")]
    public partial class Planner
    {
        [Key]
        public int IdPlanner { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int IdShift { get; set; }

        public int IdBus { get; set; }

        public int IdDriver { get; set; }

        public int IdRoute { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Shift Shift { get; set; }

        public virtual Route Route { get; set; }
    }
}
