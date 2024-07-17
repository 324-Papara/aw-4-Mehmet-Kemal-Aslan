using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Domain;

namespace Para.Api.OldControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NonController]
    public class Customers3Controller : ControllerBase
    {
        private readonly ParaMsSqlDbContext dbContext;

        public Customers3Controller(ParaMsSqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<List<Customer>> Get()
        {
            var entityList1 = await dbContext.Set<Customer>().Include(x => x.CustomerAddresses).Include(x => x.CustomerPhones).Include(x => x.CustomerDetails).ToListAsync();
            var entityList2 = await dbContext.Customers.Include(x => x.CustomerAddresses).Include(x => x.CustomerPhones).Include(x => x.CustomerDetails).ToListAsync();
            return entityList1;
        }

        [HttpGet("{customerId}")]
        public async Task<Customer> Get(long customerId)
        {
            var entity = await dbContext.Set<Customer>().Include(x => x.CustomerAddresses).Include(x => x.CustomerPhones).Include(x => x.CustomerDetails).FirstOrDefaultAsync(x => x.Id == customerId);
            return entity;
        }

        [HttpPost]
        public async Task Post([FromBody] Customer value)
        {
            var entity = await dbContext.Set<Customer>().AddAsync(value);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{customerId}")]
        public async Task Put(long customerId, [FromBody] Customer value)
        {
            dbContext.Set<Customer>().Update(value);
            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{customerId}")]
        public async Task Delete(long customerId)
        {
            var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
            dbContext.Set<Customer>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
