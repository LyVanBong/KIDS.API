using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Student")]
    public class StudentController : ApiController
    {
        private H_KIDSEntities _db;

        public StudentController()
        {
            _db = new H_KIDSEntities();
        }

        /// <summary>
        /// Cập nhật lại thông tin học sinh
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update(StudentModel student)
        {
            var data1 = _db.sp_Student_Profile_sel(student.StudentId).ToList();

            if (data1.Any())
            {
                var studentRecord = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(studentRecord.Picture) ? student.StudentId.ToString() : studentRecord.Picture;

                string strm = student.Picture;
                var bytess = Convert.FromBase64String(strm);

                //var myfilename = "/StudentPhoto/" + string.Format(@"{0}", Guid.NewGuid()) + ".jpg";
                string filepath = @"C:/inetpub/HKids/school.hkids.edu.vn";
                //string filepath = @"C:/Software/SchoolKids/Main";


                using (var imageFile = new FileStream(filepath + fileName, FileMode.OpenOrCreate))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }

                var data = _db.sp_Student_Profile_Upd(student.StudentId, student.Name, student.Sex, student.Dob, student.Email, student.Address, fileName, student.NhomMau, student.GhiChu);
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
        /// lấy thông tin chi tiết của học sinh
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [Route("Profile")]
        [HttpGet]
        public IHttpActionResult Profile(Guid StudentID)
        {
            var data = _db.sp_Student_Profile_sel(StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Profile_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Profile_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        // Danh sách học sinh của lớp
        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult StudentOfClass(Guid ClassID)
        {
            var data = _db.sp_Teacher_StudentList_sel(ClassID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_StudentList_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_StudentList_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        // Danh sách phụ hunh
        [Route("Select/Parent")]
        [HttpGet]
        public IHttpActionResult Parent(Guid StudentID)
        {
            var data = _db.sp_Students_Parents_sel(StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Students_Parents_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Students_Parents_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// <summary>
        /// lấy thông tin chi tiết của phụ huynh
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [Route("ParentProfile")]
        [HttpGet]
        public IHttpActionResult ParentProfile(String ParentID)
        {
            var data = _db.sp_Students_Parents_Detail_sel(ParentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Students_Parents_Detail_sel_Result>>()
                {
                    Code = 16,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Students_Parents_Detail_sel_Result>>()
                {
                    Code = -17,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }

        /// <summary>
        /// Cập nhật lại thông tin Phụ huynh
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("ParentUpdate")]
        [HttpPost]
        public IHttpActionResult ParentUpdate()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            var files = httpRequest.Files;
            var student = new ParentModel();
            var formData = httpRequest.Form ?? new System.Collections.Specialized.NameValueCollection();
            foreach (var key in formData.AllKeys)
            {
                foreach (var val in formData.GetValues(key))
                {
                    switch (key)
                    {
                        case "ID":
                            student.ID = Guid.Parse(val);
                            break;
                        case "Name":
                            student.Name = val;
                            break;
                        case "Sex":
                            student.Sex =int.Parse(val);
                            break;
                        case "Dob":
                            student.Dob = DateTime.Parse(val); ;
                            break;
                        case "Mobile":
                            student.Mobile = val;
                            break;
                        case "Email":
                            student.Email = val;
                            break;
                        case "Address":
                            student.Address = val;
                            break;
                        case "Picture":
                            student.Picture = val;
                            break;


                    }

                }
            }




            var data1 = _db.sp_Students_Parents_Detail_sel(student.ID.ToString()).ToList();

            if (data1.Any())
            {
                var studentRecord = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(studentRecord.Picture) ? student.ID.ToString() : studentRecord.Picture;

                string strm = student.Picture;
                //var bytess = Convert.FromBase64String(strm);

                //var myfilename = "/StudentPhoto/" + string.Format(@"{0}", Guid.NewGuid()) + ".jpg";
                //string filepath = @"C:/inetpub/HKids/school.hkids.edu.vn";

                var myfilename = "/StudentPhoto/" + string.Format(@"{0}", Guid.NewGuid()) + ".jpg";

                string filepath = @"C:/Software/SchoolKids/Main" + myfilename;

                //if (!Directory.Exists(@"C:\inetpub\HKids\school.hkids.edu.vn\NewsUpload"))
                //{
                //    Directory.CreateDirectory(@"C:\inetpub\HKids\school.hkids.edu.vn\NewsUpload");
                //}
                try
                {
                    if (files?.Count > 0)
                    {
                        using (var imageFile = new FileStream(filepath, FileMode.OpenOrCreate))
                        {
                            //imageFile.Write(bytess, 0, bytess.Length);
                            //imageFile.Flush();
                            var file = files[0];
                            file.SaveAs(filepath);


                        }
                    }
                }
                catch (Exception e)
                {
                   return BadRequest(e.ToString());
                }

                //imageFile.Write(bytess, 0, bytess.Length);
                //imageFile.Flush();




                var data = _db.sp_Student_ParentProfile_Upd(student.ID, student.Name, student.Sex, student.Dob, student.Mobile, student.Email, student.Address, myfilename);
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
                return Ok(new ResponseModel<int>
                {
                    Code = 30,
                    Message = "SUCCESSFULLY",
                    Data = 12,
                });
            }

        }
    }
}