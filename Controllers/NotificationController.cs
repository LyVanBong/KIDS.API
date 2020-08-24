using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KIDS.API.Database;
using KIDS.API.Models;

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

        [Route("Count")]
        [HttpGet]
        public IHttpActionResult GetCountNotification(Guid classId, Guid shoolId)
        {
            var noti = _db.sp_Teachers_Notifications(classId, shoolId).ToList();
            if (noti.Any())
            {
                var notiCount = noti.Where(x => x.Views == 0);
                if (notiCount.Any())
                {
                    return Ok(new ResponseModel<int>()
                    {
                        Code = 155,
                        Message = "SUCCESSFULLY",
                        Data = notiCount.Count(),
                    });
                }
                else
                {
                    return Ok(new ResponseModel<string>()
                    {
                        Code = 155,
                        Message = "SUCCESSFULLY",
                        Data = "",
                    });
                }

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
        public IHttpActionResult GetAllNotification(Guid ClassId, Guid SchoolId)
        {
            var data = _db.sp_Teachers_Notifications(ClassId, SchoolId).ToList();
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
    }
}
