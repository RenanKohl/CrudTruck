using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.CrudTruck.Domain
{
    public class User : Entity
    {
        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

    }
}
