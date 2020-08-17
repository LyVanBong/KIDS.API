using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KIDS.API.Helpers;
namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Application")]
    public class ApplicationController : ApiController
    {
        private H_KIDSEntities _db;

        public ApplicationController()
        {
            _db = new H_KIDSEntities();
        }

        // HỌC SINH
        /// <summary>
        /// Học sinh Tạo mới đơn xin nghỉ
        /// </summary>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public IHttpActionResult InsertApplication(ApplicationModel insert)
        {
            var data = _db.sp_Student_Application_Ins(insert.FromDate, insert.ToDate, insert.Date, insert.Content, insert.StudentID, insert.ClassID);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        /// <summary>
        ///Học sinh Sửa đơn xin nghỉ
        /// </summaryApplication/Update
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult UpdateApplication(ApplicationModel update)
        {
            var data = _db.sp_Student_Application_Upd(update.ID, update.FromDate, update.ToDate, update.Date, update.Content);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        /// <summary>
        /// Xóa đơn
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult DeleteApplication([FromBody]Guid update)
        {
            var data = _db.sp_Student_Application_Del(update);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        //Hochj sinh: lấy danh sách đơn xin nghỉ theo học sinh
        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult ApplicationSelStudents(String StudentId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Student_Application_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(StudentId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Application_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Student_Application_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //GIÁO VIÊN

        //Giáo viên: lấy danh sách đơn xin nghỉ theo lớp
        [Route("Select/Class")]
        [HttpGet]
        public IHttpActionResult ApplicationSelClass(String ClassId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Application_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(ClassId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Application_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Application_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //--Giáo viên Xác nhận đơn xin nghỉ
        //--Status: false: chưa xác nhận, true: đã XN 
        //--Approver: TeacherID
        //--Description: giáo viên mô tả

        [Route("Approve")]
        [HttpPost]
        public IHttpActionResult ApproveApplication(ApplicationModel Approve)
        {
            var data = _db.sp_Teacher_ApplicationAprove_Upd(Approve.ID, Approve.Status, Approve.Approver, Approve.Description);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}