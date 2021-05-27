 using System.Net.Http;
 using System.Text.Json;
 using System.Threading.Tasks;
 using Frontend.Data;

namespace Frontend
{
     public class BackendClient
     {
         private readonly JsonSerializerOptions options = new JsonSerializerOptions()
         {
             PropertyNameCaseInsensitive = true,
             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
         };
 
         private readonly HttpClient client;
 
         public BackendClient(HttpClient client)
         {
             this.client = client;
         }
 
         public async Task<Contact[]> GetContactsAsync()
         {
             var responseMessage = await this.client.GetAsync("/contacts");
             var stream = await responseMessage.Content.ReadAsStreamAsync();
             return await JsonSerializer.DeserializeAsync<Contact[]>(stream, options);
         }
     }
}