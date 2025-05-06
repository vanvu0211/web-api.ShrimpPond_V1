using System.ComponentModel.DataAnnotations;

namespace ShrimpPond.API.Authorization.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
    }
}
