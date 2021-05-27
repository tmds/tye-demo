using System.ComponentModel.DataAnnotations;

namespace Frontend.Data
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}