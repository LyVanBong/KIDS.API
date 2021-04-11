using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var teacher = new TeacherModel();
            var formData = httpRequest.Form ?? new System.Collections.Specialized.NameValueCollection();
            foreach (var key in formData.AllKeys)
            {
                foreach (var val in formData.GetValues(key))
                {
                    switch (key)
                    {
                        case "TeacherId":
                            teacher.TeacherId = Guid.Parse(val);
                            break;
                        case "Name":
                            teacher.Name = val;
                            break;
                        case "Sex":
                            teacher.Sex = int.Parse(val);
                            break;
                        case "Dob":
                            teacher.Dob = DateTime.Parse(val); ;
                            break;
                        case "Phone":
                            teacher.Phone = val;
                            break;
                        case "Email":
                            teacher.Email = val;
                            break;
                        case "Address":
                            teacher.Address = val;
                            break;
                    }

                }
            }

            var data1 = _db.sp_Teacher_Profile_sel(teacher.TeacherId).ToList();

            if (data1.Any())
            {
                var studentRecord = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(studentRecord.Picture) ? teacher.TeacherId.ToString() : studentRecord.Picture;
                if (fileName.Length <= Guid.Empty.ToString().Length)
                    fileName = "/TeacherPhoto/" + fileName + ".jpg";

                var myfilename = "/TeacherPhoto/" + string.Format(@"{0}", Guid.NewGuid()) + ".jpg";

                string filepath = @"C:/inetpub/HKids/school.hkids.edu.vn" + myfilename;

                if (httpRequest.Files.Count > 0)
                {
                    var fileImage = httpRequest.Files;
                    var newPathFileName = "C:/inetpub/HKids/school.hkids.edu.vn/TeacherPhoto/" + teacher.TeacherId +
                                          "_" + fileImage[0].FileName;
                    fileImage[0].SaveAs(newPathFileName);
                    if (File.Exists(newPathFileName))
                    {
                        File.Delete(filepath);
                        fileName = "/TeacherPhoto/" + teacher.TeacherId +
                                  "_" + fileImage[0].FileName;
                    }
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