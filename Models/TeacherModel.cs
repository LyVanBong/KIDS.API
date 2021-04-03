using System;

namespace KIDS.API.Models
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        //public string Picture { get; set; }
    }
}