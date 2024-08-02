using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Token
{
    public interface ITokenService
    {
        Task<string> GetToken(User user);
    }
}
