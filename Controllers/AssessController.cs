using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Assess")]
    public class AssessController : ApiController
    {
        private H_KIDSEntities _db;

        public AssessController()
        {
            _db = new H_KIDSEntities();
        }

        //Bảng đánh giá (load Combobox)

        [Route("Select/Plan")]
        [HttpGet]
        public IHttpActionResult AssessPlan(Guid SchoolID, Guid ClassID)
        {
            var data = _db.sp_Student_AssessPlan_sel(SchoolID, ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_AssessPlan_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_AssessPlan_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        //danh sách  học sinh trong bảng đánh giá

        [Route("Select")]
        [HttpGet]
        public IHttpActionResult AssessSel(Guid AssessID)
        {
            var data = _db.sp_Teacher_Assess_sel(AssessID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assess_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assess_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        // danh sách chỉ tiêu đánh giá của mỗi học sinh

        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult AssessDetail(Guid AssessID, Guid StudentID)
        {
            var data = _db.sp_Teacher_Assess_Student_sel(AssessID, StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assess_Student_sel_Result>>()
                {
                    Code = 10,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assess_Student_sel_Result>>()
                {
                    Code = -11,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
        // Cập nhật kết quả đánh giá của mỗi học sinh
        [Route("UpdateAssess")]
        [HttpPost]
        public IHttpActionResult UpdateAssess(AssessModel update)
        {
            var data = _db.sp_Teacher_Assess_Student_Result_Upd(update.ID, update.Result);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}