namespace MaClassePA.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public interface ITimeTracker
    {
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }
    }
}
