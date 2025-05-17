namespace ShrimpPond.API.Model
{
    public class ResetPassword
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string CacheKey { get; set; } = string.Empty;
    }
}
