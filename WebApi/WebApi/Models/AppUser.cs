using Microsoft.AspNetCore.Identity;

namespace WebApi.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
