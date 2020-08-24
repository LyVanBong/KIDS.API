using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/Daily")]
    public class DailyController : ApiController
    {
        private H_KIDSEntities _db;

        public DailyController()
        {
            _db = new H_KIDSEntities();
        }
        // HỌC SINH
        //bài học buổi sáng theo học sinh, ngày
        [Route("Morning/Student")]
        [HttpGet]
        public IHttpActionResult MorningStudent(DateTime date, Guid ClassID, Guid StudentID)
        {
            var data = _db.sp_Student_Study_Morning_sel(date, ClassID, StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Study_Morning_sel_Result>>()
                {
                    Code = 18,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Study_Morning_sel_Result>>()
                {
                    Code = -19,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        //bài học buổi chiểu theo học sinh, ngày
        [Route("Afternoon/Student")]
        [HttpGet]
        public IHttpActionResult AfternoonStudent(DateTime date, Guid ClassID, Guid StudentID)
        {
            var data = _db.sp_Student_Study_Afternoon_sel(date, ClassID, StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Study_Afternoon_sel_Result>>()
                {
                    Code = 18,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Study_Afternoon_sel_Result>>()
                {
                    Code = -19,
                    Message = "FAILED",
                    Data = null
                });
            }
        }

        // Học sinh ngủ trong ngày
        [Route("SelectSleep/Student")]
        [HttpGet]
        public IHttpActionResult Student_DailySleep(DateTime Date, Guid StudentID)
        {
            var data = _db.sp_Student_Daily_Sleep_sel(Date, StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Sleep_sel_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Sleep_sel_Result>>()
                {
                    Code = -8,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
        // Học sinh ăn trong ngày
        [Route("SelectMeal/Student")]
        [HttpGet]
        public IHttpActionResult Student_DailyMeal(DateTime Date, Guid StudentID, Guid Grade)
        {
            var data = _db.sp_Student_Daily_Meal_sel(Date, StudentID, Grade).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Meal_sel_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Meal_sel_Result>>()
                {
                    Code = -8,
                    Message = "FAILED",
                    Data = null,
                });
            }

        }
        // Học sinh vệ sinh trong ngày
        [Route("SelectHygiene/Student")]
        [HttpGet]
        public IHttpActionResult Student_DailyHygiene(DateTime Date, Guid StudentID)
        {
            var data = _db.sp_Student_Daily_Hygiene_sel(Date, StudentID).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Hygiene_sel_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Student_Daily_Hygiene_sel_Result>>()
                {
                    Code = -8,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }


        //GIÁO VIÊN

        // lấy danh sách học sinh có mặt trong ngày
        [Route("SelectAttendance")]
        [HttpGet]
        public IHttpActionResult Student_DailySel(DateTime Date, Guid ClassID)
        {
            var data = _db.sp_Teacher_Student_Daily_sel(ClassID, Date).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Student_Daily_sel_Result>>()
                {
                    Code = 7,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Teacher_Student_Daily_sel_Result>>()
                {
                    Code = -8,
                    Message = "FAILED",
                    Data = null,
                });
            }
        }
        // Cập nhật học buổi sáng
        [Route("UpdateStudyMorning")]
        [HttpPost]
        public IHttpActionResult UpdateStudyMorning(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_StudyAM_Upd(update.ID, update.UserCreate, update.StudyCommentAM);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật học buổi chiều
        [Route("UpdateStudyAfternoon")]
        [HttpPost]
        public IHttpActionResult UpdateStudyAfternoon(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_StudyPM_Upd(update.ID, update.UserCreate, update.StudyCommentPM);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật nhận xét Ăn
        [Route("UpdateMeal")]
        [HttpPost]
        public IHttpActionResult UpdateMeal(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_Meal_Upd(update.ID, update.UserCreate, update.MealComment0, update.MealComment1,
                update.MealComment2, update.MealComment3, update.MealComment4, update.MealComment5);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật nhận xét ngủ
        [Route("UpdateSleep")]
        [HttpPost]
        public IHttpActionResult UpdateSleep(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_Sleep_Upd(update.ID, update.UserCreate, update.SleepFrom, update.SleepTo, update.SleepComment);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật nhận vệ sinh
        [Route("UpdateHygiene")]
        [HttpPost]
        public IHttpActionResult UpdateHygiene(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_Hygiene_Upd(update.ID, update.UserCreate, update.Hygiene, update.HygieneComment);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật nhận xét ngày
        [Route("UpdateAssessment_Day")]
        [HttpPost]
        public IHttpActionResult UpdateAssessment_Day(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_Assessment_Day_Upd(update.ID,update.DayComment, update.PhieuBeNgoan, update.UserCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
        // Cập nhật nhận xét tuần
        [Route("UpdateAssessment_Week")]
        [HttpPost]
        public IHttpActionResult UpdateAssessment_Week(DailyModel update)
        {
            var data = _db.sp_Teacher_Daily_Assessment_Week_Upd(update.ID, update.WeekComment, update.WeekPhieuBeNgoan, update.UserCreate);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}
