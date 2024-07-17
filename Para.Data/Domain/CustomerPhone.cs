using Para.Base.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.Domain
{
    [Table("CustomerPhone", Schema = "dbo")]

    public class CustomerPhone : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string CountyCode { get; set; } // TUR
        public string Phone { get; set; }
        public bool IsDefault { get; set; }
    }
}
