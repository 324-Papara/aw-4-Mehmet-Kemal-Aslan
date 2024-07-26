using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Data.CustomerReportRepository;
using Para.Data.Domain;
using Para.Schema.Models;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController : ControllerBase
    {
        private readonly ICustomerReportRepository _customerReportRepository;

        public CustomerReportController(ICustomerReportRepository customerReportRepository)
        {
            _customerReportRepository = customerReportRepository;
        }

        [HttpGet("GetCustomersReport")]
        public async Task<IActionResult> GetActiveCustomers()
        {
            IEnumerable<CustomerReportRequestResponse> customers = await _customerReportRepository.GetCustomerReport();
            return Ok(customers);
        }
    }
}
