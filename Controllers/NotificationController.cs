using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Notification")]
    public class NotificationsController : ApiController
    {
        private H_KIDSEntities _db;

        public NotificationsController()
        {
            _db = new H_KIDSEntities();
        }
        //Insert thông báo t_Notice
        [Route("InsertNotice")]
        [HttpPost]
        public IHttpActionResult InsertNotice(NoticeModel insert)
        {
            var data = _db.sp_Notice_Ins(insert.NoticeID, insert.SchoolID, insert.ClassID, insert.StudentID, insert.ParentID, insert.Type, insert.Approve);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // giáo viên, nhân viên VP
        [Route("Count")]
        [HttpGet]
        public IHttpActionResult GetCountNotification(string teacherId, string classId)
        {
            var noti = _db.sp_Teachers_Notifications_Count(teacherId, classId).FirstOrDefault();
            if (noti != null)
            {
                return Ok(new ResponseModel<string>()
                {
                    Code = 155,
                    Message = "SUCCESSFULLY",
                    Data = noti + "",
                });
            }
            else
            {
                return Ok(new ResponseModel<string>()
                {
                    Code = -155,
                    Message = "FAILED",
                    Data = "",
                });
            }
        }

        /// <summary>
        /// lay danh sach thong bao
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("All")]
        [HttpGet]
        public IHttpActionResult GetAllNotification(Guid ClassId, Guid TeacherId)
        {
            var data = _db.sp_Teachers_Notifications(ClassId, TeacherId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teachers_Notifications_Result>>()
                {
                    Code = 15,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
                return Ok(new ResponseModel<IEnumerable<sp_Teachers_Notifications_Result>>()
                {
                    Code = -16,
                    Message = "FAILED",
                    Data = null,
                });
        }
        // phụ huỵnh
        [Route("CountForStudent")]
        [HttpGet]
        public IHttpActionResult CountForStudent(String SchoolId, String ParentID)
        {
            var noti = _db.sp_Students_Notifications_Count(SchoolId, ParentID).FirstOrDefault();
            if (noti != null)
            {
                return Ok(new ResponseModel<string>()
                {
                    Code = 155,
                    Message = "SUCCESSFULLY",
                    Data = noti + "",
                });
            }
            else
            {
                return Ok(new ResponseModel<string>()
                {
                    Code = -155,
                    Message = "FAILED",
                    Data = "",
                });
            }
        }

        [Route("Student")]
        [HttpGet]
        public IHttpActionResult GetStudentNotification(Guid SchoolId, Guid ParentID)
        {
            var data = _db.sp_Students_Notifications(SchoolId, ParentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Students_Notifications_Result>>()
                {
                    Code = 15,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
                return Ok(new ResponseModel<IEnumerable<sp_Students_Notifications_Result>>()
                {
                    Code = -16,
                    Message = "FAILED",
                    Data = null,
                });
        }
    }
}