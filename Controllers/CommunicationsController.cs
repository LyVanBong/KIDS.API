using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KIDS.API.Helpers;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Communication")]
    public class CommunicationController : ApiController
    {
        private H_KIDSEntities _db;

        public CommunicationController()
        {
            _db = new H_KIDSEntities();
        }

        // HỌC SINH
        // Học sinh Tạo mới tin nhắn
        [Route("Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertCommunication(CommunicationModel insert)
        {
            var provider = new RestrictiveMultipartMemoryStreamProvider();

            //READ CONTENTS OF REQUEST TO MEMORY WITHOUT FLUSHING TO DISK
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (HttpContent ctnt in provider.Contents)
            {
                //now read individual part into STREAM
                var stream = await ctnt.ReadAsStreamAsync();

                if (stream.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        //TODO : Save Image
                    }
                }
            }
            var data = _db.sp_Student_Communications_Ins(insert.ClassID, insert.TeacherID, insert.Parent, insert.Content, insert.DateCreate, insert.StudentID, 2);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }

        //Học sinh Sửa tin
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult UpdateCommunication(CommunicationModel update)
        {
            var data = _db.sp_Student_Communications_Upd(update.CommunicationID, update.Content, update.DateCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Xóa tin
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult DeleteCommunication(CommunicationModel update)
        {
            var data = _db.sp_Student_Communications_Del(update.CommunicationID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        //Học sinh: lấy danh sách tin nhắn đã tạo
        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult CommunicationSelStudents(String StudentId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Student_Communications_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(StudentId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Communications_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Student_Communications_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //Học sinh: lấy danh sách tin nhắn giáo viên gửi
        [Route("Select/TeacherToStudent")]
        [HttpGet]
        public IHttpActionResult CommunicationSelTeacherToStudent(String StudentId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Student_Communications_sel_TeacherToStudent(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(StudentId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Communications_sel_TeacherToStudent_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Student_Communications_sel_TeacherToStudent_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }


        //GIÁO VIÊN
        // Giáo viên Tạo mới tin nhắn
        [Route("InsertTeacher")]
        [HttpPost]
        public IHttpActionResult InsertTeacherCommunication(CommunicationModel insert)
        {
            var data = _db.sp_Student_Communications_Ins(insert.ClassID, insert.TeacherID, insert.Parent, insert.Content, insert.DateCreate, insert.StudentID, 1);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        //Giáo viên Sửa tin
        [Route("UpdateTeacher")]
        [HttpPost]
        public IHttpActionResult UpdateTeacherCommunication(CommunicationModel update)
        {
            var data = _db.sp_Student_Communications_Upd(update.CommunicationID, update.Content, update.DateCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Giáo viên Xóa tin
        [Route("DeleteTeacher")]
        [HttpPost]
        //public IHttpActionResult DeleteTeacherCommunication([FromBody]Guid CommunicationID)
        public IHttpActionResult DeleteTeacherCommunication([FromBody] DeleteMessageModel update)
        {
            var data = _db.sp_Student_Communications_Del(update.CommunicationID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }



        //Giáo viên: lấy danh sách tin nhắn theo lớp
        [Route("Select/Class")]
        [HttpGet]
        public IHttpActionResult CommunicationSelClass(String ClassId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_CommunicationsClass_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(ClassId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_CommunicationsClass_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Communications_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //Giáo viên: lấy danh sách tin nhắn của giáo viên
        [Route("Select/Teacher")]
        [HttpGet]
        public IHttpActionResult CommunicationSelTeacher(String TeacherID)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Communications_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(TeacherID))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Communications_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Communications_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //Hiển thị bình luận của tin nhắn
        [Route("Select/Reply")]
        [HttpGet]
        public IHttpActionResult CommunicationSelReply(String Parent)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Student_Communications_sel_Reply(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(Parent))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Communications_sel_Reply_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Student_Communications_sel_Reply_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //--Giáo viên Xác nhận tin nhắn của học sinh

        [Route("Approve")]
        [HttpPost]
        public IHttpActionResult ApproveCommunication(CommunicationModel Approve)
        {
            var data = _db.sp_Teacher_CommunicationAprove_Upd(Approve.CommunicationID, Approve.IsConfirmed, Approve.Approver, Approve.Content);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}