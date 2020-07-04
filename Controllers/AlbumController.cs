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
        /// lấy danh sách album do class + school tạo
        /// </summary>
        /// <returns></returns>
        [Route("Select/All")]
        [HttpGet]
        public IHttpActionResult AlbumSelAll(string SchoolId, string ClassId)
        {
            var data = _db.sp_Album_sel_ClassAndSchool(ClassId, SchoolId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = 6,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = -7,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        /// <summary>
        /// lấy danh sách album do trường tạo
        /// </summary>
        /// <returns></returns>
        [Route("Select/School")]
        [HttpGet]
        public IHttpActionResult AlbumSelSchool(string SchoolId)
        {
            var data = _db.sp_Album_sel_ClassAndSchool(SchoolId, SchoolId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = 6,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = -7,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        /// <summary>
        /// Lấy danh sách album ảnh trong một lớp học
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult AlbumSel(string ClassID)
        {
            var data = _db.sp_Album_sel_ClassAndSchool(ClassID, ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = 6,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchool_Result>>()
                {
                    Code = -7,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        /// <summary>
        /// lấy danh sách ảnh trong album ảnh
        /// </summary>
        /// <param name="AlbumID"></param>
        /// <returns></returns>
        [Route("Detail")]
        [HttpGet]
        public IHttpActionResult AlbumDetailSel(Guid AlbumID)
        {
            var data = _db.sp_AlbumDetail_sel(AlbumID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_AlbumDetail_sel_Result>>()
                {
                    Code = 9,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_AlbumDetail_sel_Result>>()
                {
                    Code = -10,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
    }
}