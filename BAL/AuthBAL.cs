using DAL;
using MODELS;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BAL
{
    public class AuthBAL
    {
        AuthDAL dal = new AuthDAL();

        public LoggedUser Login(LoginModel user)
        {
            user.password = HashPassword(user.password);
            return dal.Login(user);
        }

        public bool EmailExists(string email)
        {
            return dal.EmailExists(email);
        }

        public bool Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                PasswordHash = HashPassword(model.Password),
                Role = "Employee",
                CreatedDate = DateTime.Now
            };
            return dal.Register(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}