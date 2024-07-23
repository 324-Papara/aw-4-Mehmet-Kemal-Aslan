using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class CustomerDetailRequest : BaseRequest
    {
        public int CustomerId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MontlyIncome { get; set; }
        public string Occupation { get; set; }
    }

    public class CustomerDetailResponse : BaseResponse
    {
        public int CustomerId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MontlyIncome { get; set; }
        public string Occupation { get; set; }
    }
}
