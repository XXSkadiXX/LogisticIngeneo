using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.User
{
    [ExcludeFromCodeCoverage]
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public double Expiration { get; set; }
    }
}
