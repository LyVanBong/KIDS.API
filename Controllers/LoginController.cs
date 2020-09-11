using KIDS.API.Database;
using KIDS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class LoginController : ApiController
    {
        private H_KIDSEntities _db;
        public LoginController()
        {
            _db = new H_KIDSEntities();
        }
        /// <summary>
        /// Api đăng nhập ứng dụng Giáo viên + Nhân viên
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] LoginModel login)
        {
            if (login != null)
            {
                var data = _db.sp_Login(login.UserName, login.Password).FirstOrDefault();
                if (data != null)
                {
                    return Ok(new ResponseModel<sp_Login_Result>()
                    {
                        Code = 0,
                        Message = "Logged in successfully",
                        Data = data,
                    });
                }
            }
            return Ok(new ResponseModel<sp_Login_Result>()
            {
                Code = -1,
                Message = "Login failed",
                Data = null,
            });
        }
        //PHỤ HUYNH login
        [Route("ParentLogin")]
        [HttpPost]
        public IHttpActionResult ParentLogin([FromBody] LoginModel login)
        //public IHttpActionResult ParentLogin(string UserName, string Password)
        {
            if (login != null)
            {
                
                var data = _db.sp_Login_Parent_2(login.UserName, login.Password).FirstOrDefault();
                if (data != null)
                {
                    return Ok(new ResponseModel<sp_Login_Parent_2_Result>()
                    {
                        Code = 0,
                        Message = "Logged in successfully",
                        Data = data,
                    });
                }
            }
            return Ok(new ResponseModel<sp_Login_Parent_2_Result>()
            {
                Code = -1,
                Message = "Login failed",
                Data = null,
            });


            //var data = _db.sp_Login_Parent_2(UserName, Password).ToList();
            //if (data.Any())
            //{
            //    return Ok(new ResponseModel<List<sp_Login_Parent_2_Result>>()
            //    {
            //        Code = 2,
            //        Message = "successfully",
            //        Data = data,
            //    });
            //}
            //return Ok(new ResponseModel<List<sp_Login_Parent_2_Result>>()
            //{
            //    Code = -3,
            //    Message = "error",
            //    Data = null,
            //});
        }

        //Phụ huynh sau khi đăng nhập, nếu có >1 học sinh thì hiển thị danh sách học sinh để chọn
        [Route("ParentShowStudentsLogin")]
        [HttpGet]
        public IHttpActionResult ParentShowStudentsLogin(string UserName)
        {
            var data = _db.sp_Login_Parent_ShowStudents_2(UserName).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Login_Parent_ShowStudents_2_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Login_Parent_ShowStudents_2_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }

        //Chọn học sinh từ danh sách
        [Route("ParentGetStudentIDLogin")]
        [HttpGet]
        public IHttpActionResult ParentGetStudentIDLogin(string StudentId)
        {
            var data = _db.sp_Login_Parent_GetStudent(StudentId).ToList();
            if (data.Any())
            {
                return Ok(new ResponseModel<List<sp_Login_Parent_GetStudent_Result>>()
                {
                    Code = 2,
                    Message = "successfully",
                    Data = data,
                });
            }
            return Ok(new ResponseModel<List<sp_Login_Parent_GetStudent_Result>>()
            {
                Code = -3,
                Message = "error",
                Data = null,
            });
        }

        [Route("ChangePassWord")]
        [HttpPost]
        public IHttpActionResult ChangePass([FromBody] LoginModel login)
        {
            var data = _db.sp_ChangePassWord(login.UserName, login.Password);
            return Ok(new ResponseModel<int>
            {
                Code = 30,
                Message = "SUCCESSFULLY",
                Data = data,
            });
        }
    }
}