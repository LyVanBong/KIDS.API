using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Health")]
    public class HealthController : ApiController
    {
        private H_KIDSEntities _db;

        public HealthController()
        {
            _db = new H_KIDSEntities();
        }

        //danh sách các đợt cân đo)
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult HealthSel(Guid StudentID)
        {
            var data = _db.sp_Student_Health_sel(StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Health_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Health_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        [Route("Insert")]
        [HttpPost]
        public IHttpActionResult Insert(HealthModel insert)
        {
         
            double BMI = insert.Width * 10000 / (insert.Height * insert.Height);
            var data = _db.sp_Student_Health_Ins(insert.StudentID, insert.ClassID, insert.Date, insert.MonthAge, insert.Height, insert.Width, BMI);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }


        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(HealthModel update)
        {
            double BMI = update.Width * 10000 / (update.Height * update.Height);
            var data = _db.sp_Student_Health_Upd(update.ID, update.Height,
                update.Width, BMI);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }


        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult Delete(HealthModel update)
        {
            var data = _db.sp_Student_Health_Del(update.ID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}