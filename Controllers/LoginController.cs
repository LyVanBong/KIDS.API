using KIDS.API.Database;
using KIDS.API.Models;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class LoginController : ApiController
    {
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
                var db = new H_KIDSEntities();
                var data = db.sp_Login(login.UserName, login.Password).FirstOrDefault();
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