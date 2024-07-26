using Para.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Models
{
    public class CustomerReportRequestResponse : BaseRequest
    {
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }


        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MontlyIncome { get; set; }
        public string Occupation { get; set; }


        public string CountyCode { get; set; }
        public string Phone { get; set; }


        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
    }
}
