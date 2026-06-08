using System.Linq;
using MODELS;

namespace DAL
{
    public class AuthDAL
    {
        public LoggedUser Login(LoginModel user)
        {
            using (var db = new PizzaPOSEntities())
            {
                var dbUser = db.Users
                               .FirstOrDefault(u => u.Email == user.email);

                if (dbUser == null)
                    return null;

                if (dbUser.PasswordHash != user.password)
                    return null;

                return new LoggedUser
                {
                    name = dbUser.UserName,
                    role = dbUser.Role
                };
            }
        }

        public bool EmailExists(string email)
        {
            using (var db = new PizzaPOSEntities())
            {
                return db.Users.Any(u => u.Email == email);
            }
        }

        public bool Register(User user)
        {
            using (var db = new PizzaPOSEntities())
            {
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
        }
    }
}