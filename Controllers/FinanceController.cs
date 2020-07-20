using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Finance")]
    public class FinanceController : ApiController
    {
        private H_KIDSEntities _db;

        public FinanceController()
        {
            _db = new H_KIDSEntities();
        }
       
        //Thông báo thu tiền
      
        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult FinanceSel(Guid StudentID)
        {
            var data = _db.sp_Student_HocPhi_sel(StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_HocPhi_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_HocPhi_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
       
        // Chi tiết các khoản thu
       
        [Route("Select/Detail")]
        [HttpGet]
        public IHttpActionResult FinanceDetail(Guid DotThu_HocSinhID)
        {
            var data = _db.sp_KhoanThu_DotThu_ChiTiet_sel(DotThu_HocSinhID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_KhoanThu_DotThu_ChiTiet_sel_Result>>()
                {
                    Code = 10,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_KhoanThu_DotThu_ChiTiet_sel_Result>>()
                {
                    Code = -11,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
    }
}