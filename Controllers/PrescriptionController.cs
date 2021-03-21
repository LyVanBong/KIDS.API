using KIDS.API.Configurations;
using KIDS.API.Database;
using KIDS.API.Helpers;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Drawing;
using System.IO;
using System.Web;
using Newtonsoft.Json;

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
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult CreatePrescription()
        {
            Guid MasterID = Guid.NewGuid();
            var insert = new MedicineTicketModel();

            var httpRequest = HttpContext.Current.Request;
            var files = httpRequest.Files;
            var formData = httpRequest.Form ?? new System.Collections.Specialized.NameValueCollection();
            foreach (var key in formData.AllKeys)
            {
                foreach (var val in formData.GetValues(key))
                {
                    switch (key) 
                    {
                        case "FromDate":
                            insert.FromDate = DateTime.Parse(val);
                            break;
                        case "ToDate":
                            insert.ToDate = DateTime.Parse(val);
                            break;
                        case "Date":
                            insert.Date = DateTime.Parse(val);
                            break;
                        case "Content":
                            insert.Content = val;
                            break;
                        case "StudentID":
                            insert.StudentID = Guid.Parse(val);
                            break;
                        case "ClassID":
                            insert.ClassID = Guid.Parse(val);
                            break;
                        case "MedicineList":
                            insert.MedicineList = string.IsNullOrEmpty(val) ? new List<MedicineDetailTicketModel>() : JsonConvert.DeserializeObject< List<MedicineDetailTicketModel>>(val);
                            break;
                    }
                        
                }
            }

            var data = _db.sp_Student_Prescription_Ins(MasterID, insert.FromDate, insert.ToDate, insert.Date, insert.Content, insert.StudentID, insert.ClassID);
            if (insert.MedicineList?.Any() == true)
            {
                for(int i=0; i< insert.MedicineList.Count; i++)
                {
                    var item = insert.MedicineList[i];
                    var myfilename = string.Format(@"{0}", Guid.NewGuid());
                    string filepath = @"C:\inetpub\Kids\school.hkids.edu.vn\NewsUpload\" + myfilename + ".jpg";
                    var file = files[i];
                    file.SaveAs(filepath);
                    var data1 = _db.sp_Student_Prescription_Detail_Ins(myfilename, MasterID, item.Name, item.Unit, item.Note);
                }
            }

            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }
        //HỌC SINH VÀ GIÁO VIÊN

        [HttpGet()]
        [Route("PrescriptionDetail")]
        public IHttpActionResult PrescriptionDetail(Guid id)
        {
            try
            {
                var db = new H_KIDSEntities();
                var masterData = db.sp_Student_Teacher_Prescription_sel(id).ToList();
                var detaildata = db.sp_Teacher_Prescription_Detail_sel(id).ToList();
                var masterDataItem = masterData.FirstOrDefault();
                if (masterDataItem != null)
                {
                    var result = new MedicineTicketModel
                    {
                        Id = masterDataItem.ID,
                        FromDate = masterDataItem.FromDate,
                        ToDate = masterDataItem.ToDate,
                        Date = masterDataItem.Date,
                        Content = masterDataItem.Content,
                        StudentID = masterDataItem.StudentID,
                        ClassID = masterDataItem.ClassID,
                        Status = masterDataItem.Status,
                        Approver = masterDataItem.Approver,
                        Description = masterDataItem.Description,
                        MedicineList = new List<MedicineDetailTicketModel>()
                    };

                    if (detaildata?.Any() == true)
                    {
                        foreach (var item in detaildata)
                        {
                            result.MedicineList.Add(new MedicineDetailTicketModel
                            {
                                Id = item.ID,
                                Picture = item.Picture,
                                Note = item.Description,
                                Unit = item.Unit
                            });
                        }
                    }

                    return Ok(new ResponseModel<MedicineTicketModel>()
                    {
                        Code = 26,
                        Message = "SUCCESSFULLY",
                        Data = result,
                    });
                }
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
                {
                    Code = -6,
                    Message = "Get data prescription detail error",
                    Data = null,
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
                {
                    Code = -6,
                    Message = "Get data prescription detail error",
                    Data = null,
                });
            }
        }

        ////HỌC SINH VÀ GIÁO VIÊN
        //[HttpGet]
        //[Route("PrescriptionDetail")]
        //public IHttpActionResult PrescriptionDetail(Guid PrescriptionID)
        //{
        //    var db = new H_KIDSEntities();
        //    var data = db.sp_Teacher_Prescription_Detail_sel(PrescriptionID).ToList();

        //    if (data.Any())
        //    {
        //        return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
        //        {
        //            Code = 5,
        //            Message = "Get data prescription detail successfully",
        //            Data = data,
        //        });
        //    }
        //    return Ok(new ResponseModel<List<sp_Teacher_Prescription_Detail_sel_Result>>()
        //    {
        //        Code = -6,
        //        Message = "Get data prescription detail error",
        //        Data = null,
        //    });
        //}
        // CHI TIẾT
        /// <summary>
        /// Thêm Detail mới
        /// </summary>
        /// <param name="albumDetail"></param>
        /// <returns></returns>
        [Route("InsertDetail")]
        [HttpPost]
        public IHttpActionResult InsertDetail(PrescriptionDetailModel Prescription)
        {
            var data = _db.sp_Student_Prescription_Detail_Ins(Prescription.Picture, Prescription.PrescriptionID, Prescription.Name, Prescription.Unit, Prescription.Description);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        //// HỌC SINH
        ///// <summary>
        ///// Học sinh Tạo mới đơn thuốc (master)
        ///// </summary>
        ///// <returns></returns>
        //[Route("Insert")]
        //[HttpPost]
        //public IHttpActionResult InsertPrescription(PrescriptionModel insert)
        //{
        //    Guid MasterID = Guid.NewGuid();
        //    var data = _db.sp_Student_Prescription_Ins(insert.FromDate, insert.ToDate, insert.Date, insert.Content, insert.StudentID, insert.ClassID);
        //    return Ok(new ResponseModel<int>
        //    {
        //        Code = 30,
        //        Message = "SUCCESSFULLY",
        //        Data = data,
        //    });
        //    return null;
        //}


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
        [HttpPost]
        public IHttpActionResult DeletePrescription(PrescriptionModel update)
        {
            var data = _db.sp_Student_Prescription_Del(update.ID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }


        /// <summary>
        /// update album
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [Route("UpdateDetail")]
        [HttpPost]
        public IHttpActionResult UpdateDetail(PrescriptionDetailModel Prescription)
        {
            var data = _db.sp_Student_Prescription_Detail_Upd(Prescription.ID, Prescription.Picture, Prescription.Name, Prescription.Unit, Prescription.Description);
            return Ok(new ResponseModel<int>
            {
                Code = 27,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }


        // Xóa đơn thuốc Detail
        // <param name="Id"></param>
        [Route("DeleteDetail")]
        [HttpPost]
        public IHttpActionResult DeletePrescriptionDetail(PrescriptionDetailModel Prescription)
        {
            var data = _db.sp_Student_Prescription_Detail_Del(Prescription.ID);
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = data,
            });
        }

        [Route("UpdatePrescription")]
        [HttpPost]
        public IHttpActionResult UpdatePrescription([FromBody] MedicineTicketModel data)
        {
            var dataMaster = _db.sp_Student_Prescription_Upd(data.Id, data.FromDate, data.ToDate, data.Date, data.Content);

            if (data.MedicineList?.Any() == true)
            {
                var addList = data.MedicineList.Where(x => x.Action == (int)UserAction.Insert).ToList();
                if (addList.Any())
                {
                    //insert into DB
                    foreach (var item in addList)
                    {
                        var data1 = _db.sp_Student_Prescription_Detail_Ins(item.Picture, item.Id, item.Name, item.Unit, item.Note);
                    }
                }
                var addListupdate = data.MedicineList.Where(x => x.Action == (int)UserAction.Update).ToList();
                if (addList.Any())
                {
                    //insert into DB
                    foreach (var item in addListupdate)
                    {
                        var data1 = _db.sp_Student_Prescription_Detail_Upd(item.Id, item.Picture, item.Name, item.Unit, item.Note);
                    }
                }
                var addListDelete = data.MedicineList.Where(x => x.Action == (int)UserAction.Delete).ToList();
                if (addList.Any())
                {
                    //insert into DB
                    foreach (var item in addListupdate)
                    {
                        var data1 = _db.sp_Student_Prescription_Detail_Del(item.Id);
                    }
                }
            }
            return Ok(new ResponseModel<int>
            {
                Code = 24,
                Message = AppConstants.Successfully,
                Data = 1,
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


    }
}