using EveCM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data
{
    public class EveCMContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<CharacterDetails> CharacterDetails { get; set; }

        public EveCMContext(DbContextOptions<EveCMContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
            .Property(u => u.Id)
            .HasMaxLength(450);

            builder.Entity<IdentityRole>()
                .Property(r => r.Id)
                .HasMaxLength(450);
            
            builder.Entity<IdentityUserLogin<string>>()
                .Property(l => l.LoginProvider)
                .HasMaxLength(450);

            builder.Entity<IdentityUserLogin<string>>()
                .Property(l => l.ProviderKey)
                .HasMaxLength(450);

            builder.Entity<IdentityUserToken<string>>()
                .Property(t => t.LoginProvider)
                .HasMaxLength(450);

            builder.Entity<IdentityUserToken<string>>()
                .Property(t => t.Name)
                .HasMaxLength(450);
        }
    }
}
