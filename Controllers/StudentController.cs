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

                var myfilename = string.Format(@"{0}", Guid.NewGuid());
                string filepath = @"C:/inetpub/Kids/school.hkids.edu.vn";
                //string filepath = @"C:/Software/SchoolKids/Main";


                using (var imageFile = new FileStream(filepath + fileName, FileMode.OpenOrCreate))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }

                var data = _db.sp_Student_Profile_Upd(student.StudentId, student.Name, student.Sex, student.Dob, student.Email, student.Address, fileName);
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
        public IHttpActionResult ParentUpdate(ParentModel student)
        {

            var data1 = _db.sp_Students_Parents_Detail_sel(student.ID.ToString()).ToList();

            if (data1.Any())
            {
                var studentRecord = data1.FirstOrDefault();
                var fileName = string.IsNullOrEmpty(studentRecord.Picture) ? student.ID.ToString() : studentRecord.Picture;

                string strm = student.Picture;
                var bytess = Convert.FromBase64String(strm);

                var myfilename = string.Format(@"{0}", Guid.NewGuid());
                string filepath = @"C:/inetpub/Kids/school.hkids.edu.vn";
                //string filepath = @"C:/Software/SchoolKids/Main";


                using (var imageFile = new FileStream(filepath + fileName, FileMode.OpenOrCreate))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }


                var data = _db.sp_Student_ParentProfile_Upd(student.ID, student.Name, student.Sex, student.Dob, student.Mobile, student.Email, student.Address, fileName);
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
    }
}