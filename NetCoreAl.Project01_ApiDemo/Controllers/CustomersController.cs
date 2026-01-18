using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAl.Project01_ApiDemo.Context;

namespace NetCoreAl.Project01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CustomerList() {
            var value = _context.Customers.ToList();
            return Ok(value);
        }
    }
}
