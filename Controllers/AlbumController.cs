using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Album")]
    public class AlbumController : ApiController
    {
        private H_KIDSEntities _db;

        public AlbumController()
        {
            _db = new H_KIDSEntities();
        }

        /// <summary>
        /// Lấy danh sách album ảnh trong một lớp học
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult AlbumSel(Guid ClassID)
        {
            var data = _db.sp_Album_sel(ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_Result>>()
                {
                    Code = 6,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_Result>>()
                {
                    Code = -7,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
    }
}