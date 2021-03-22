using System;

namespace KIDS.API.Models
{
    public class HealthModel
    {
        public Guid ID { get; set; }

        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public DateTime Date { get; set; }
        public double MonthAge { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
     
    }
}