using System.ComponentModel.DataAnnotations;

namespace IdentityWithSpieser.Models
{
    public class ApplicationRoleDto
    {
        [Required]
        public string Name { get; set; }

    }
}
