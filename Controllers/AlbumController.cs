using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KIDS.API.Configurations;

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
        /// Thêm album mới
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [Route("InsertAlbum")]
        [HttpPost]
        public IHttpActionResult InsertAlbum(AlbumModel album)
        {
            var data = _db.sp_Album_Ins(album.ClassID, album.Thumbnail, album.Description, album.DateCreate, album.UserCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        /// <summary>
        /// update album
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        [Route("UpdateAlbum")]
        [HttpPost]
        public IHttpActionResult UpdateAlbum(AlbumModel album)
        {
            var data = _db.sp_Album_Upd(album.AlbumID, album.Thumbnail, album.Description, album.UserCreate, album.DateCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        /// <summary>
        /// xao mot album anh
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [Route("DeleteAlbum")]
        [HttpPost]
        public IHttpActionResult DeleteAlbum(Guid albumId)
        {
            var data = _db.sp_Album_Del(albumId);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
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
        /// <summary>
        /// Thêm album Detail mới
        /// </summary>
        /// <param name="albumDetail"></param>
        /// <returns></returns>
        [Route("InsertAlbumImage")]
        [HttpPost]
        public IHttpActionResult InsertAlbumImage(AlbumImageModel albumImage)
        {
            var data = _db.sp_AlbumImage_Ins(albumImage.AlbumID, albumImage.ImageURL, albumImage.Description, albumImage.Sort);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        /// <summary>
        /// update album
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [Route("UpdateAlbumImage")]
        [HttpPost]
        public IHttpActionResult UpdateAlbumImage(AlbumImageModel albumImage)
        {
            var data = _db.sp_AlbumImage_Upd(albumImage.ImageID, albumImage.ImageURL, albumImage.Description, albumImage.Sort);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        /// <summary>
        /// xao mot album anh
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [Route("DeleteAlbumImage")]
        [HttpPost]
        public IHttpActionResult DeleteAlbumImage(Guid imageId)
        {
            var data = _db.sp_AlbumImage_Del(imageId);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
    }
}