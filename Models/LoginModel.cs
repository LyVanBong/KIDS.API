namespace KIDS.API.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Tài khoản người dùng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        public string Password { get; set; }
    }
}