using System;

namespace KIDS.API.Models
{
    public class AssessModel
    {
        public Guid ID { get; set; }
        public Guid NamHoc { get; set; }
        public Guid ClassID { get; set; }
        public Guid StudentID { get; set; }
        public Guid AssessID { get; set; }
        public string STT { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public Guid ParentID { get; set; }
        public bool Result { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserCreate { get; set; }

    }
}