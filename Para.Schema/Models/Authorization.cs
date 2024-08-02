using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class Authorization
    {
        public class AuthorizationRequest : BaseRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }


        public class AuthorizationResponse : BaseResponse
        {
            public DateTime ExpireTime { get; set; }
            public string AccessToken { get; set; }
            public string UserName { get; set; }
        }
    }
}
