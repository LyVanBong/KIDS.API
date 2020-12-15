using System;

namespace KIDS.API.Models
{
    public class NoticeModel
    {
        public Guid NoticeID { get; set; }
        public Guid SchoolID { get; set; }
        public Guid ClassID { get; set; }
        public Guid TeacherID { get; set; }
        public Guid StudentID { get; set; }
        public Guid ParentID { get; set; }
        public Int32 Type { get; set; }
        public Boolean Approve { get; set; }
      
    }
}