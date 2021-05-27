using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.Data;
using System.Collections.Generic;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BackendClient _contactClient;

        public IndexModel(BackendClient contactClient)
        {
            _contactClient = contactClient;
        }

        public IList<Contact> Contacts { get; private set; }

        public async Task OnGetAsync()
        {
            Contacts = await _contactClient.GetContactsAsync();
        }
    }
}