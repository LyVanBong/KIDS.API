using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
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
        [HttpPut]
        public IHttpActionResult Update(TeacherModel teacher)
        {
            var data = _db.sp_Teacher_Profile_Upd(teacher.TeacherId, teacher.Name, teacher.Sex, teacher.Dob, teacher.Phone, teacher.Email, teacher.Address, teacher.Picture);
            if(data>0)
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
    }
}
