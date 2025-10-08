using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        
        [StringLength(50)]
        public string GroupName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Course { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new();
    }
}