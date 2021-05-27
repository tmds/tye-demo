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
    public class ContactsController : ControllerBase
    {
        private readonly ContactDbContext _db;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ContactDbContext db, ILogger<ContactsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _db.Contacts.AsNoTracking().ToListAsync();
        }
    }
}
