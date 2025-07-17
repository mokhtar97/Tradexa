

using Microsoft.AspNetCore.Identity;

namespace Tradexa.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
