using KIDS.API.Database;
using KIDS.API.Models;
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
        /// login app parent
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("LoginParent")]
        [HttpPost]
        public IHttpActionResult LoginParent([FromBody] LoginModel login)
        {
            if (login != null)
            {
                var data = _db.sp_Login_Parent(login.UserName, login.Password).FirstOrDefault();
                if (data != null)
                {
                    return Ok(new ResponseModel<sp_Login_Parent_Result>()
                    {
                        Code = 0,
                        Message = "Logged in successfully",
                        Data = data,
                    });
                }
            }
            return Ok(new ResponseModel<string>()
            {
                Code = -1,
                Message = "Login failed",
                Data = null,
            });
        }
        /// <summary>
        /// Api đăng nhập ứng dụng
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
    }
}