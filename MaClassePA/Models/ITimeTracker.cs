namespace MaClassePA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITimeTracker
    {
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }
    }
}
