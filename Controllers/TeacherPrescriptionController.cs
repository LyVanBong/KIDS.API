using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class TeacherPrescriptionController : ApiController
    {
        /// <summary>
        /// Api lấy danh sách đơn dặn thuộc của lớp
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [Route("Prescription")]
        [HttpGet]
        public IHttpActionResult Get(Guid TeacherId, Guid ClassId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Prescription_sel(ClassId, TeacherId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_sel_Result>>()
                {
                    Code = 1,
                    Message = "Get data prescription successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Prescription_sel_Result>>()
            {
                Code = -2,
                Message = "Get data prescription error",
                Data = null,
            });
        }

        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PrescriptionDetail")]
        public IHttpActionResult PrescriptionDetail(Guid PrescriptionID)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Prescription_Detail_sel(PrescriptionID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
                {
                    Code = 5,
                    Message = "Get data prescription detail successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
            {
                Code = -6,
                Message = "Get data prescription detail error",
                Data = null,
            });
        }
    }
}