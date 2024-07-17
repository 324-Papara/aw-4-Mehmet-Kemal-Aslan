using Para.Data.Domain;
using Para.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
        Task CompleteWithTransaction();
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
        IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
        IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
    }
}
