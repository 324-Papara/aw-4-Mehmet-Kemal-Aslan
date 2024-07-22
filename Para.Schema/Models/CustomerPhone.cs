using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class CustomerPhoneRequest : BaseRequest
    {
        public int CustomerId { get; set; }
        public string CountyCode { get; set; }
        public string Phone { get; set; }
        public bool IsDefault { get; set; }
    }


    public class CustomerPhoneResponse : BaseResponse
    {
        public int CustomerId { get; set; }
        public string CountyCode { get; set; }
        public string Phone { get; set; }
        public bool IsDefault { get; set; }
    }
}
