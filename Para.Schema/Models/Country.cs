using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class CountryRequest : BaseRequest
    {
        public string CountyCode { get; set; }
        public string Name { get; set; }
    }


    public class CountryResponse : BaseResponse
    {
        public string CountyCode { get; set; }
        public string Name { get; set; }
    }
}
