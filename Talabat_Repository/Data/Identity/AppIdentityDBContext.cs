using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models.Identity;

namespace Talabat_Repository.Data.Identity
{
    public class AppIdentityDBContext:IdentityDbContext<AppUser>
    {
        private readonly DbContextOptions<AppIdentityDBContext> _options;

        public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options):base(options)
        {
            _options = options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Adresses");
                
            //builder.Entity<AppUser>()
            //.HasOne(u => u.Address)
            //.WithOne(a => a.appUser)
            //.HasForeignKey<Address>(a => a.AppUserId) // Foreign key is in Address
            //.IsRequired(false); // Address must have an AppUser

        }



    }
}
