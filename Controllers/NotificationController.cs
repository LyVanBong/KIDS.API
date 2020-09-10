﻿using System;
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
        //[Route("CountAll")]
        //[HttpGet]
        //public IHttpActionResult CountAllNotification(Guid ClassId, Guid SchoolId)
        //{
        //    var data = _db.sp_Teachers_Notifications_Count(ClassId, SchoolId).ToList();
        //    if (data.Any())
        //    {
        //        return Ok(new ResponseModel<IEnumerable<sp_Teachers_Notifications_Count_Result>>()
        //        {
        //            Code = 15,
        //            Message = "SUCCESSFULLY",
        //            Data = data,
        //        });
        //    }
        //    else
        //        return Ok(new ResponseModel<IEnumerable<sp_Teachers_Notifications__Result>>()
        //        {
        //            Code = -16,
        //            Message = "FAILED",
        //            Data = null,
        //        });
        //}
        [Route("Student")]
        [HttpGet]
        public IHttpActionResult GetStudentNotification(Guid ClassId, Guid SchoolId, Guid StudentId)
        {
            var data = _db.sp_Students_Notifications(ClassId, SchoolId, StudentId).ToList();
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
