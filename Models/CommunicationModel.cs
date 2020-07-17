using System;

namespace KIDS.API.Models
{
    public class CommunicationModel
    {
        public Guid CommunicationID { get; set; }
        public Guid ClassID { get; set; }
        public Guid TeacherID { get; set; }
        public int Type { get; set; }
        public Guid Parent { get; set; }
        public String Content { get; set; }
        public DateTime DateCreate { get; set; }
        public String StudentID { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid Approver { get; set; }
        public DateTime ApproverDate { get; set; }
        public int Views { get; set; }
    }
}