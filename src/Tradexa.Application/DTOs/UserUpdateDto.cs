using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tradexa.Application.DTOs
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public List<string> Roles { get; set; } = new();

        // UI Preferences
        public string PreferredLanguage { get; set; }  // "en" or "ar"
        public string Theme { get; set; }              // "light" or "dark"
        public bool ShowHeader { get; set; }
        public bool ShowFooter { get; set; }
        public bool ShowSidebar { get; set; }
    }
}
