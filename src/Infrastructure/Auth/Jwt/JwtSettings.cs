using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace ScreenDrafts.Api.Infrastructure.Auth.Jwt;
public class JwtSettings : IValidatableObject
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int TokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Key))
        {
            yield return new ValidationResult("Key is required.", new[] { nameof(Key) });
        }
    }
}
