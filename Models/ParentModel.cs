using System;

namespace KIDS.API.Models
{
    public class ParentModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime Dob { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}