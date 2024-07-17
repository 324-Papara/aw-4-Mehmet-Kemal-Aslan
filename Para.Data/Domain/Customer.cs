using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Para.Base.Entity;

namespace Para.Data.Domain
{
    [Table("Customer", Schema = "dbo")]
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual List<CustomerAddress> CustomerAddresses { get; set; }
        public virtual List<CustomerPhone> CustomerPhones { get; set; }
        public virtual CustomerDetail CustomerDetails { get; set; }
    }
}
