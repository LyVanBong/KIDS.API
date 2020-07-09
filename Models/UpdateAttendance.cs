using System;

namespace KIDS.API.Models
{
    public class UpdateMorning
    {
        public Guid Id { get; set; }
        public bool CoMat { get; set; }
        public bool NghiCoPhep { get; set; }
        public bool NghiKhongPhep { get; set; }
        public bool DiMuon { get; set; }
        public string Comment { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class UpdateAfternoon
    {
        public Guid Id { get; set; }
        public DateTime Leave { get; set; }
        public bool LeaveLate { get; set; }
        public string LeaveComment { get; set; }
        public Guid NguoiDon { get; set; }
        public Guid TeacherId { get; set; }
    }
}