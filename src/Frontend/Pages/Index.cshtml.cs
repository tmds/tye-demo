using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Frontend.Data;
using System.Collections.Generic;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BackendClient _customerClient;

        public IndexModel(BackendClient customerClient)
        {
            _customerClient = customerClient;
        }

        public IList<Customer> Customers { get; private set; }

        public async Task OnGetAsync()
        {
            Customers = await _customerClient.GetCustomersAsync();
        }
    }
}