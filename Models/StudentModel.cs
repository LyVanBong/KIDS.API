using System;

namespace KIDS.API.Models
{
    public class StudentModel
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public int NhomMau { get; set; }
        public string GhiChu { get; set; }
    }
}