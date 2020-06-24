using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class AttendanceController : ApiController
    {
        /// <summary>
        /// Điểm danh học sinh
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [Route("Attendance")]
        [HttpGet]
        public IHttpActionResult Attendance(Guid ClassId, DateTime Date)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Attendance_sel(ClassId, Date).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Attendance_sel_Result>>()
                {
                    Code = 3,
                    Message = "",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Application_sel_Result>>()
            {
                Code = -4,
                Message = "Get data attendance student error",
                Data = null,
            });
        }

        /// <summary>
        /// đếm tổng số HS, có mặt, nghỉ phép.. của lớp trong ngay
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("AttendanceCount")]
        public IHttpActionResult AttendanceCount(Guid ClassId, DateTime Date)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Attendance_Count_sel(ClassId, Date).ToList();
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
    }
}