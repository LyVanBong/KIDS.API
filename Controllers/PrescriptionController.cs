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
    [RoutePrefix("api/v1/Prescription")]
    public class PrescriptionController : ApiController
    {
        private H_KIDSEntities _db;

        public PrescriptionController()
        {
            _db = new H_KIDSEntities();
        }

        // HỌC SINH
        /// <summary>
        /// Học sinh Tạo mới đơn thuốc
        /// </summary>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public IHttpActionResult InsertPrescription(PrescriptionModel insert)
        {
            var data = _db.sp_Student_Prescription_Ins(insert.FromDate, insert.ToDate, insert.Date, insert.Content, insert.StudentID, insert.ClassID);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        /// <summary>
        ///Học sinh Sửa đơn thuốc
        /// </summaryPrescription/Update
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult UpdatePrescription(PrescriptionModel update)
        {
            var data = _db.sp_Student_Prescription_Upd(update.ID, update.FromDate, update.ToDate, update.Date, update.Content);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Xóa đơn thuốc Master
        // <param name="PrescriptionId"></param>
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult DeletePrescription([FromBody]Guid ID)
        {
            var data = _db.sp_Student_Prescription_Del(ID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        // Xóa đơn thuốc Detail
        // <param name="Id"></param>
        [Route("DeleteDetail")]
        [HttpDelete]
        public IHttpActionResult DeletePrescriptionDetail([FromBody] Guid ID)
        {
            var data = _db.sp_Student_Prescription_Detail_Del(ID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        //Hochj sinh: lấy danh sách đơn thuốc theo học sinh
        [Route("Select/Student")]
        [HttpGet]
        public IHttpActionResult PrescriptionSelStudents(String StudentId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Student_Prescription_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(StudentId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Student_Prescription_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Student_Prescription_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //GIÁO VIÊN

        //Giáo viên: lấy danh sách đơn thuốc theo lớp
        [Route("Select/Class")]
        [HttpGet]
        public IHttpActionResult PrescriptionSelClass(String ClassId)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Prescription_sel(Guid.Parse(HashFunctionHelper.GetValueOrDBNull(ClassId))).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_sel_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Prescription_sel_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }
        //--Giáo viên Xác nhận đơn thuốc
        //--Status: false: chưa xác nhận, true: đã XN 
        //--Approver: TeacherID
        //--Description: giáo viên mô tả

        [Route("Approve")]
        [HttpPost]
        public IHttpActionResult ApprovePrescription(PrescriptionModel Approve)
        {
            var data = _db.sp_Teacher_PrescriptionAprove_Upd(Approve.ID, Approve.Status, Approve.Approver, Approve.Description);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        //HỌC SINH VÀ GIÁO VIÊN
        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        /// <param name="PrescriptionID Key Parent"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PrescriptionDetail")]
        public IHttpActionResult PrescriptionDetail(Guid PrescriptionID)
        {
            var db = new H_KIDSEntities();
            var data = db.sp_Teacher_Prescription_Detail_sel(PrescriptionID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
                {
                    Code = 5,
                    Message = "Get data prescription detail successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
            {
                Code = -6,
                Message = "Get data prescription detail error",
                Data = null,
            });
        }
    }
}