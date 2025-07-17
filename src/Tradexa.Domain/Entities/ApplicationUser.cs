using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Domain.Entities
{

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? PreferredLanguage { get; set; }
    public string? LayoutPreference { get; set; }
    public string? ThemePreference { get; set; }
}

}
