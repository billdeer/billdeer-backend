using Billdeer.Core.Entities.Abstract;
using Billdeer.Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.Entities.Dtos.UserDtos
{
    public class UserLoginResDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AccessToken accessToken { get; set; }
    }
}
