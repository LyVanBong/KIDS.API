using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public IHttpActionResult CreateAlbum()
        {
            Guid MasterID = Guid.NewGuid();
            var insert = new AlbumTicketModel();

            var httpRequest = HttpContext.Current.Request;
            var files = httpRequest.Files;
            var formData = httpRequest.Form ?? new System.Collections.Specialized.NameValueCollection();
            foreach (var key in formData.AllKeys)
            {
                foreach (var val in formData.GetValues(key))
                {
                    switch (key)
                    {
                        case "ClassID":
                            insert.ClassID = Guid.Parse(val);
                            break;
                        case "Thumbnail":
                            insert.Thumbnail = val;
                            break;
                        case "Description":
                            insert.Description = val;
                            break;
                        case "DateCreate":
                            insert.DateCreate = DateTime.Parse(val); ;
                            break;
                        case "UserCreate":
                            insert.UserCreate = Guid.Parse(val);
                            break;

                        case "MedicineList":
                            insert.AlbumList = string.IsNullOrEmpty(val) ? new List<AlbumDetailTicketModel>() : JsonConvert.DeserializeObject<List<AlbumDetailTicketModel>>(val);
                            break;
                    }

                }
            }
            var data = _db.sp_Album_Ins(MasterID, insert.ClassID, insert.Thumbnail, insert.Description, insert.DateCreate, insert.UserCreate);
         
            if (insert.AlbumList?.Any() == true)
            {
                for (int i = 0; i < insert.AlbumList.Count; i++)
                {
                    var item = insert.AlbumList[i];
                    var myfilename = string.Format(@"{0}", Guid.NewGuid());
                    string filepath = @"C:\inetpub\Kids\school.hkids.edu.vn\AlbumUpload\" + myfilename + ".jpg";
                    var file = files[i];
                    file.SaveAs(filepath);
                  
                    var data1 = _db.sp_AlbumImage_Ins(MasterID, myfilename, item.Description, item.Sort);
                }
            }

            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        ///// <summary>
        ///// Thêm album mới
        ///// </summary>
        ///// <param name="album"></param>
        ///// <returns></returns>
        //[Route("InsertAlbum")]
        //[HttpPost]
        //public IHttpActionResult InsertAlbum(AlbumModel album)
        //{

        //    var data = _db.sp_Album_Ins(album.ClassID, album.Thumbnail, album.Description, album.DateCreate, album.UserCreate);
        //    return Ok(new ResponseModel<int>
        //    {
        //        Code = 27,
        //        Message = AppConstants.Successfully,
        //        Data = data,
        //    });
        //}

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
        public IHttpActionResult DeleteAlbum([FromBody] Guid update)
        {
            var data = _db.sp_Album_Del(update);
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
            var data = _db.sp_Album_sel_ClassAndSchool(SchoolId, ClassId).ToList();
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
        public IHttpActionResult AlbumSelSchool(string SchoolId, string ClassId)
        {
            var data = _db.sp_Album_sel_ClassAndSchool(SchoolId, ClassId).ToList();
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
        /// // hiện tại không dùng
        ///GIÁO VIÊN Lấy danh sách album ảnh trong một lớp học
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult AlbumSel(String SchoolID, string ClassId)
        {
            var data = _db.sp_Album_sel_ClassAndSchool(SchoolID, ClassId).ToList();
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
        ///HỌC SINH Lấy danh sách album ảnh trong một lớp học THEO PHỤ HUYNH ID
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("SelectParent")]
        [HttpGet]
        public IHttpActionResult AlbumSelByParent(string ParentId, string SchoolID)
        {
            var data = _db.sp_Album_sel_ClassAndSchoolForParent(ParentId, SchoolID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchoolForParent_Result>>()
                {
                    Code = 6,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Album_sel_ClassAndSchoolForParent_Result>>()
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
        public IHttpActionResult AlbumDetailSel(Guid AlbumID, Guid GiaoVien_PhuHuynhClick)
        {
            var data = _db.sp_AlbumDetail_sel(AlbumID, GiaoVien_PhuHuynhClick).ToList();
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
        public IHttpActionResult DeleteAlbumImage([FromBody] Guid update)
        {
            var data = _db.sp_AlbumImage_Del(update);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
    }
}