using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models.Identity;

namespace Talabat_Repository.Data.Identity
{
    public static class AppIdentityDBcontextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _UserManager)
        {
            if (_UserManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "MahaAhmed",
                    Email = "Maha_ahmed@gmaill.com",
                    UserName = "Omar1525",
                    PhoneNumber = "01069701020",
                };

                var result = await _UserManager.CreateAsync(user,"Pa$$w0rd");
                //if (!result.Succeeded)
                //{
                //    foreach (var error in result.Errors)
                //    {
                //        // Log or throw an error
                //        Console.WriteLine(error.Description);
                //    }
                //}

            }
            //if (_UserManager.Users.Count() == 1)
            //{
            //    var user = new AppUser()
            //    {
            //        EmailConfirmed = true,
            //    };
            //    var result = await _UserManager.CreateAsync(user);

            //}
        }
    }
}
