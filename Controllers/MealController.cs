using KIDS.API.Configurations;
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
       
        //Thông báo thu tiền
      
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult MealControllerSel()
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
       
    }
}