using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Attendance")]
    public class AttendanceController : ApiController
    {
        private H_KIDSEntities _db;

        public AttendanceController()
        {
            _db = new H_KIDSEntities();
        }

        /// <summary>
        /// lớp trông học sinh về muộn, lấy danh sách điểm danh về có cột LeaveLate = true
        /// </summary>
        /// <returns></returns>
        [Route("LeaveLate")]
        [HttpGet]
        public IHttpActionResult AttendanceLeaveLate(Guid classId, DateTime date)
        {
            var data = _db.sp_Teacher_AttendanceLeaveLate_sel(classId, date).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_AttendanceLeaveLate_sel_Result>>
                {
                    Code = 24,
                    Message = "SUCCESSFULLY",
                    Data = data
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_AttendanceLeaveLate_sel_Result>>
                {
                    Code = -25,
                    Message = "FAILED",
                    Data = null
                });
            }
        }

        /// <summary>
        /// cập nhật điểm danh về, ghi chú trong danh sách học sinh lớp trong ngày
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [Route("UpdateAfternoon")]
        [HttpPost]
        public IHttpActionResult UpdateAfternoon(UpdateAfternoon update)
        {
            var data = _db.sp_Teacher_AttendanceLeave_Upd(update.Id, update.Leave, update.LeaveLate,
                update.LeaveComment, update.NguoiDon, update.TeacherId);
            return Ok(new ResponseModel<int>()
            {
                Code = 23,
                Message = "SUCCESSFULLY",
                Data = data
            });
        }

        /// <summary>
        /// điểm danh về, lây danh sách học sinh có mặt trong danh sách điểm danh sáng
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [Route("Afternoon")]
        [HttpGet]
        public IHttpActionResult Afternoon(Guid classId, DateTime date)
        {
            var data = _db.sp_Teacher_AttendanceLeaves_sel(classId, date).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_AttendanceLeaves_sel_Result>>()
                {
                    Code = 22,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_AttendanceLeaves_sel_Result>>()
            {
                Code = -23,
                Message = "Get data attendance student error",
                Data = null,
            });
        }

        /// <summary>
        /// cập nhật điểm danh đến, ghi chú trong danh sách học sinh lớp trong ngày
        /// </summary>
        /// <returns></returns>
        [Route("UpdateMorning")]
        [HttpPost]
        public IHttpActionResult Update(UpdateMorning update)
        {
            var data = _db.sp_Teacher_AttendanceArrive_Upd(update.Id, update.CoMat, update.NghiCoPhep, update.NghiKhongPhep, update.DiMuon, update.Comment, update.TeacherId);
            return Ok(new ResponseModel<int>()
            {
                Code = 21,
                Message = "SUCCESSFULLY",
                Data = data
            });
        }

        /// <summary>
        /// Điểm danh đến lớp, sau khi tạo điểm danh theo ngày, mặc định lấy toàn bộ số học sinh trong lớp
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [Route("Morning")]
        [HttpGet]
        public IHttpActionResult Attendance(Guid ClassId, DateTime Date)
        {
            var data = _db.sp_Teacher_AttendanceArrive_sel(ClassId, Date).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_AttendanceArrive_sel_Result>>()
                {
                    Code = 3,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_AttendanceArrive_sel_Result>>()
            {
                Code = -4,
                Message = "Get data attendance student error",
                Data = null,
            });
        }

        /// <summary>
        /// đếm tổng số HS, có mặt, nghỉ phép.. của lớp trong khoản thời gian
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public IHttpActionResult AttendanceCount(Guid ClassId, DateTime FromDate, DateTime ToDate)
        {
            var data = _db.sp_Teacher_Attendance_Count_sel(ClassId, FromDate, ToDate).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Attendance_Count_sel_Result>>()
                {
                    Code = 4,
                    Message = "Get data attendance count successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Application_sel_Result>>()
            {
                Code = -5,
                Message = "Get data attendance count error",
                Data = null,
            });
        }

        /// <summary>
        /// đếm tổng số HS, có mặt, nghỉ phép.. của 1 học sinh trong khoản thời gian
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("StudentCount")]
        public IHttpActionResult AttendanceStudentCount(Guid ClassId, Guid StudentId, DateTime FromDate, DateTime ToDate)
        {
            var data = _db.sp_Student_Attendance_sel(ClassId, StudentId, FromDate, ToDate).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Attendance_sel_Result>>()
                {
                    Code = 4,
                    Message = "Get data attendance count successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Application_sel_Result>>()
            {
                Code = -5,
                Message = "Get data attendance count error",
                Data = null,
            });
        }
    }
}