using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string? Name { get; set; }
        
        public int Age { get; set; }
        

        public Group? Group { get; set; }
    }
}