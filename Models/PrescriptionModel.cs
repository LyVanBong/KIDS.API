using System;
using System.Collections.Generic;

namespace KIDS.API.Models
{
    public class PrescriptionModel
    {
        public Guid ID { get; set; }
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

    public class MedicineTicketModel
    {
        public Guid? Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public Boolean Status { get; set; }
        public Guid Approver { get; set; }
        public String Description { get; set; }
        public List<MedicineDetailTicketModel> MedicineList { get; set; }
    }

    public class MedicineDetailTicketModel
    {
        public Guid? Id { get; set; }
        public string Picture { get; set; }
        public string Note { get; set; }    
        public string Unit { get; set; }
        public string Name { get; set; }
        public int Action { get; set; } = (int)UserAction.Update;
    }

    public enum UserAction
    {
        Insert,
        Update,
        Delete
    }
}