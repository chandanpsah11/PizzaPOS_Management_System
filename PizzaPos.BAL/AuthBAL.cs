using System;
using DAL;
using MODELS;

namespace PizzaPos.BAL
{
    public class AuthBAL
    {
        Auth auth = new Auth();

        public LoggedUser Login(LoginModel user)
        {
            return auth.Login(user);
        }
    }
}
