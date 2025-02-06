using System.ComponentModel.DataAnnotations;

namespace IdentityWithSpieser.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }

    }
}
