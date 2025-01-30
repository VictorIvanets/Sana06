using Microsoft.AspNetCore.Identity;

namespace MF.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
