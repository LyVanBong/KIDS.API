using System;

namespace KIDS.API.Models
{
    public class DailyModel
    {
        public Guid ID { get; set; }
        public Guid SchoolID { get; set; }
        public Guid UserCreate { get; set; }
        public Guid StudentID { get; set; }
        public DateTime Date { get; set; }
        public bool CoMat { get; set; }
        public bool NghiCoPhep { get; set; }
        public bool NghiKhongPhep { get; set; }
        public bool DiMuon { get; set; }
        public int Hygiene { get; set; }
        public DateTime Leave { get; set; }
        public bool LeaveLate { get; set; }
        public Guid NguoiDon { get; set; }
        public DateTime SleepFrom { get; set; }
        public DateTime SleepTo { get; set; }
        public string Description { get; set; }

        public string MealComment0 { get; set; }
        public string MealComment1 { get; set; }
        public string MealComment2 { get; set; }
        public string MealComment3 { get; set; }
        public string MealComment4 { get; set; }
        public string MealComment5 { get; set; }
        public string ArriveComment { get; set; }
        public string LeaveComment { get; set; }
        public string StudyCommentAM { get; set; }
        public string StudyCommentPM { get; set; }
        public string SleepComment { get; set; }
        public string HygieneComment { get; set; }
        public string OverallComment { get; set; }
        public string DayComment { get; set; }
        public string PhieuBeNgoan { get; set; }
        public string WeekComment { get; set; }
        public string WeekPhieuBeNgoan { get; set; }
        public Guid WeekCommentCate { get; set; }
        public Guid ClassID { get; set; }
        public Guid DiemKiemTraID { get; set; }
        public double Diem1 { get; set; }
        public double Diem2 { get; set; }
        public double Diem3 { get; set; }
        public double Diem4 { get; set; }
        public double DiemTB { get; set; }
        public string NhanXetKiemTra { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ClassName { get; set; }
        public string TenMonAn { get; set; }
    }
}