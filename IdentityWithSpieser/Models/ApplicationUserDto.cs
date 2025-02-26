using System.ComponentModel.DataAnnotations;

namespace IdentityWithSpieser.Models
{
    public class ApplicationUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public ApplicationUserDto(){}

        public ApplicationUserDto(ApplicationUser user)
        {
            if (user is not null)
            {
                Name = user.UserName!;
                Email = user.Email!;
                //Password = user.PasswordHash; 
                Age = user.Age;
                PhoneNumber = user.PhoneNumber!;
            }
        }
    }

    
}
