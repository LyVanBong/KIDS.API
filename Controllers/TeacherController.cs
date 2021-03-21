using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Teacher")]
    public class TeacherController : ApiController
    {
        private H_KIDSEntities _db;

        public TeacherController()
        {
            _db = new H_KIDSEntities();
        }

        /// <summary>
        /// Cập nhật lại thông tin giao viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(TeacherModel teacher)
        {
            var data1 = _db.sp_Student_Profile_sel(teacher.TeacherId).ToList();

            if (data1.Any())
            {
                var studentRecord = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(studentRecord.Picture) ? teacher.TeacherId.ToString() : studentRecord.Picture;

                string strm = teacher.Picture;
                var bytess = Convert.FromBase64String(strm);

                var myfilename = string.Format(@"{0}", Guid.NewGuid());
                string filepath = @"C:/inetpub/Kids/school.hkids.edu.vn";
                //string filepath = @"C:/Software/SchoolKids/Main";


                using (var imageFile = new FileStream(filepath + fileName, FileMode.OpenOrCreate))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }


                var data = _db.sp_Teacher_Profile_Upd(teacher.TeacherId, teacher.Name, teacher.Sex, teacher.Dob, teacher.Phone, teacher.Email, teacher.Address, fileName);
                if (data > 0)
                {
                    return Ok(new ResponseModel<int>()
                    {
                        Code = 18,
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
                        Data = data,
                    });
                }
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
        /// lấy thông tin chi tiết của giáo viên
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [Route("Profile")]
        [HttpGet]
        public IHttpActionResult Profile(Guid teacherId)
        {
            var data = _db.sp_Teacher_Profile_sel(teacherId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Profile_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Profile_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// Danh sách giáo viên trong lớp

        [Route("Select/Teacher")]
        [HttpGet]
        public IHttpActionResult TeacherOfClass(Guid ClassID)
        {
            var data = _db.sp_Teacher_Assign_sel(ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assign_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Assign_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
    }
}