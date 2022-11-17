using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Models
{
    public class User:IdentityUser
    {
        public List<Purshes> Purshes { get; set; } = new();
    }
}
