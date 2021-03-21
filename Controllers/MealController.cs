using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Meal")]
    public class MealControllerController : ApiController
    {
        private H_KIDSEntities _db;

        public MealControllerController()
        {
            _db = new H_KIDSEntities();
        }

        //danh sách các bữa ăn (sáng, trưa....)
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult MealSel()
        {
            var data = _db.sp_DinhDuong_BuaAn_sel().ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_DinhDuong_BuaAn_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_DinhDuong_BuaAn_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        //danh sách các món ăn (cháo, cơm..) từ bữa ăn (sáng. chiều...)
        [Route("Select/MonAn")]
        [HttpGet]
        public IHttpActionResult MonAnSel(Guid Khoi, DateTime Ngay)
        {
            var data = _db.sp_SelectMonAnFromBuaAn_sel(Khoi, Ngay).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_SelectMonAnFromBuaAn_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_SelectMonAnFromBuaAn_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        //Lấy tên món ăn từ ID món ăn
        [Route("Select/TenMonAn")]
        [HttpGet]
        public IHttpActionResult TenMonAnSel(Guid ID)
        {
            var data = _db.sp_SelectTenMonAn_sel(ID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_SelectTenMonAn_sel_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_SelectTenMonAn_sel_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
    }
}