using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/News")]
    public class NewsController : ApiController
    {
        private H_KIDSEntities _db;

        public NewsController()
        {
            _db = new H_KIDSEntities();
        }
        /// <summary>
        /// lấy danh sách các tin tức
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult NewsSel(Guid ClassID)
        {
            var data = _db.sp_News_sel(ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_Result>>()
                {
                    Code = -8,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
        /// <summary>
        /// lấy chi tiết tin tức
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        [Route("Detail")]
        [HttpGet]
        public IHttpActionResult NewsDetail(Guid NewsID)
        {
            var data = _db.sp_NewsDetail_sel(NewsID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_NewsDetail_sel_Result>>()
                {
                    Code = 10,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_NewsDetail_sel_Result>>()
                {
                    Code = -11,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
    }
}