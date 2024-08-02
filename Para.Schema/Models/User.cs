using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class UserRequest : BaseRequest
    {
        public int? CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class UserResponse : BaseResponse
    {
        public int? CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int Status { get; set; }

        public string CustomerName { get; set; }
        public string CustomerIdentityNumber { get; set; }
        public int CustomerNumber { get; set; }
    }
}
