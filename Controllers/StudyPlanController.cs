using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/StudyPlan")]
    public class StudyPlanController : ApiController
    {
        private H_KIDSEntities _db;

        public StudyPlanController()
        {
            _db = new H_KIDSEntities();
        }
        /// <summary>
        /// bài học buổi sáng theo lớp, ngày
        /// </summary>
        /// <param name="date"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        [Route("Morning")]
        [HttpGet]
        public IHttpActionResult Morning(DateTime date, Guid classId)
        {
            var data = _db.sp_Study_Plan_Morning_sel(date, classId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Morning_sel_Result>>()
                {
                    Code = 18,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Morning_sel_Result>>()
                {
                    Code = -19,
                    Message = "FAILED",
                    Data = null
                });
            }
        }
        /// <summary>
        /// select bài học buổi chiều theo lớp, ngày
        /// </summary>
        /// <param name="date"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        [Route("Afternoon")]
        [HttpGet]
        public IHttpActionResult Afternoon(DateTime date, Guid classId)
        {
            var data = _db.sp_Study_Plan_Afternoon_sel(date, classId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Afternoon_sel_Result>>()
                {
                    Code = 19,
                    Message = "SUCCESSFULLY",
                    Data = data,
                });
            }
            else
            {
                return Ok(new ResponseModel<IEnumerable<sp_Study_Plan_Afternoon_sel_Result>>()
                {
                    Code = -20,
                    Message = "FAILED",
                    Data = null
                });
            }
        }

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
        public IHttpActionResult UpdateStudyMorning(StudyPlanModel update)
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
        public IHttpActionResult UpdateStudyAfternoon(StudyPlanModel update)
        {
            var data = _db.sp_Teacher_Daily_StudyAM_Upd(update.ID, update.UserCreate, update.StudyCommentPM);
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
        public IHttpActionResult UpdateMeal(StudyPlanModel update)
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
        public IHttpActionResult UpdateSleep(StudyPlanModel update)
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
        public IHttpActionResult UpdateHygiene(StudyPlanModel update)
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
        public IHttpActionResult UpdateAssessment_Day(StudyPlanModel update)
        {
            var data = _db.sp_Teacher_Daily_Assessment_Day_Upd(update.ID, update.PhieuBeNgoan, update.DayComment, update.UserCreate);
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
        public IHttpActionResult UpdateAssessment_Week(StudyPlanModel update)
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
