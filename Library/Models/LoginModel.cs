using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Library.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginModel
    {
        [Required]
        [MaxLength(10)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
