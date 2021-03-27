using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Tạo mới tin tức
        /// </summary>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public IHttpActionResult InsertNews(UpdateNewsModel insert)
        {
            string strm = insert.ImageUrl;
            var bytess = Convert.FromBase64String(strm);
            var myfilename = string.Format(@"{0}", Guid.NewGuid());

            //string filepath = @"C:\inetpub\HKids\school.hkids.edu.vn\NewsUpload\" + myfilename + ".jpg";
           // string filepath = @"C:\Software\SchoolKids\Main\NewsUpload\1.jpg";

            string filepath = @"C:\inetpub\HKids\school.hkids.edu.vn\NewsUpload\" + myfilename + ".jpg";

            using (var imageFile = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }

            var data = _db.sp_News_Ins(insert.Title, insert.Content, insert.ClassId, myfilename, insert.DateCreate, insert.UserCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }

        /// <summary>
        /// Cập nhân tin tức
        /// </summaryNews/Update
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult UpdateNews(UpdateNewsModel update)
        {
            var data1 = _db.sp_Student_Profile_sel(update.NewsId).ToList();

            if (data1.Any())
            {
                var Record = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(Record.Picture) ? update.NewsId.ToString() : Record.Picture;

                string strm = update.ImageUrl;
                var bytess = Convert.FromBase64String(strm);

                var myfilename = string.Format(@"{0}", Guid.NewGuid());
                //string filepath = @"C:/inetpub/HKids/school.hkids.edu.vn";

                string filepath = @"C:/Software/SchoolKids/Main";

                using (var imageFile = new FileStream(filepath + fileName, FileMode.OpenOrCreate))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }

                var data = _db.sp_News_Upd(update.NewsId, update.Title, update.Content, fileName, update.DateCreate,
                update.UserCreate);
                return Ok(new ResponseModel<int>
                {
                    Code = 30,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });

            }
            else
            {
                return Ok(new ResponseModel<int>()
                {
                    Code = -19,
                    Message = "FAILED",
                    Data = -1,
                });
            }
        }

        /// <summary>
        /// Xóa tin tức
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        [Route("DeleteNews")]
        [HttpPost]
        public IHttpActionResult DeleteNews(UpdateNewsModel update)
        {
            var data = _db.sp_News_Del(update.NewsId);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        /// <summary>
        ///GIÁO VIÊN lấy danh sách thông báo do trường và lợp tạo ra
        /// </summary>
        /// <returns></returns>
        [Route("Select/All")]
        [HttpGet]
        public IHttpActionResult NewsSelAll(string ClassId, string SchoolId)
        {
            var data = _db.sp_News_sel_ClassAndSchool(ClassId, SchoolId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchool_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchool_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// <summary>
        ///PHỤ HUYNH lấy danh sách thông báo do trường và lợp tạo ra THEO PHU HUYNH ID
        /// </summary>
        /// <returns></returns>
        [Route("SelectParent")]
        [HttpGet]
        public IHttpActionResult NewsSelByParent(string ParentId, string SchoolId)
        {
            var data = _db.sp_News_sel_ClassAndSchoolForParent(ParentId, SchoolId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchoolForParent_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchoolForParent_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// <summary>
        /// lấy all news theo trường tạo ra
        /// </summary>
        /// <returns></returns>
        [Route("Select/School")]
        [HttpGet]
        public IHttpActionResult NewsSelSchool(string ClassId, string SchoolId)
        {
            var data = _db.sp_News_sel_ClassAndSchool(ClassId, SchoolId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchool_Result>>()
                {
                    Code = 11,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassAndSchool_Result>>()
                {
                    Code = -12,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// <summary>
        /// lấy danh sách các tin tức theo lớp
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult NewsSel(string ClassID, string SchoolID)
        {
            var data = _db.sp_News_sel_ClassID(ClassID, SchoolID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassID_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_News_sel_ClassID_Result>>()
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
        public IHttpActionResult NewsDetail(Guid NewsID, Guid GiaoVien_PhuHuynhClick)
        {
            var data = _db.sp_NewsDetail_sel(NewsID, GiaoVien_PhuHuynhClick).ToList();
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