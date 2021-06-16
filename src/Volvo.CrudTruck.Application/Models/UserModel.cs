using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.CrudTruck.Application.Models
{
    public class UserModel
    {
        public class Request
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
        public class Response
        {
            public Response(string userName, string created, string expiration, string accessToken)
            {
                Name = userName;
                Created = created;
                Expiration = expiration;
                AccessToken = accessToken;
            }
            public string Name { get; set; }
            public string Created { get; private set; }
            public string Expiration { get; private set; }
            public string AccessToken { get; private set; }
        }
    }
}
