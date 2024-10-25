using Microsoft.AspNetCore.Identity;
using Talabat_Core.Models.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Talabat_API.Helper
{
    public static class UserManagerExtenstion
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User) 
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = userManager.Users.Include(u => u.Address).FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
