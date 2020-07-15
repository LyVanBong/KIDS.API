using System;

namespace KIDS.API.Models
{
    public class PrescriptionModel
    {
        public Guid ID { get; set; }
        public Guid PrescriptionID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public Boolean Status { get; set; }
        public Guid Approver { get; set; }
        public String Description { get; set; }
    }
}