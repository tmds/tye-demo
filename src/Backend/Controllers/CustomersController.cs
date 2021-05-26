using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDbContext _db;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(CustomerDbContext db, ILogger<CustomersController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _db.Customers.AsNoTracking().ToListAsync();
        }
    }
}
