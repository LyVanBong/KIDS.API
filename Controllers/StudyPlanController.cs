using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/StudyPlan")]
    public class StudyPlanController : ApiController
    {
        private H_KIDSEntities _db;

        public StudyPlanController()
        {
            _db = new H_KIDSEntities();
        }
     

        //GIÁO VIÊN
        //bài học buổi sáng theo lớp, ngày
        [Route("Morning")]
        [HttpGet]
        public IHttpActionResult Morning(DateTime date, Guid classId)
        {
            var data = _db.sp_Study_Plan_Morning_sel(date, classId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Morning_sel_Result>>()
                {
                    Code = 18,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Morning_sel_Result>>()
                {
                    Code = -19,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        /// <summary>
        /// select bài học buổi chiều theo lớp, ngày
        /// </summary>
        /// <param name="date"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        [Route("Afternoon")]
        [HttpGet]
        public IHttpActionResult Afternoon(DateTime date, Guid classId)
        {
            var data = _db.sp_Study_Plan_Afternoon_sel(date, classId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Afternoon_sel_Result>>()
                {
                    Code = 19,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Afternoon_sel_Result>>()
                {
                    Code = -20,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
   
    }
}
