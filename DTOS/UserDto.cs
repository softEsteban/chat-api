using System.ComponentModel.DataAnnotations;

namespace ChatApi.DTOS
{
    public class UserDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage="Username must be at least (2) and maximum (1)")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
