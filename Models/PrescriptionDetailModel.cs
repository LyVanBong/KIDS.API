using System;

namespace KIDS.API.Models
{
    public class PrescriptionDetailModel
    {
        public Guid ID { get; set; }
        public Guid PrescriptionID { get; set; }
        public String Picture { get; set; }
        public String Name { get; set; }
        public String Unit { get; set; }
        public String Description { get; set; }
       
    }
}