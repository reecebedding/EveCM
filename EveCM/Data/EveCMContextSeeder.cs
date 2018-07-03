using EveCM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data
{
    public class EveCMContextSeeder
    {
        private readonly EveCMContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public EveCMContextSeeder(EveCMContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<EveCMContextSeeder> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public void Seed()
        {
            SeedRole("Admin");
            SeedRole("Corp_Member");

            ApplicationUser administratorUser = new ApplicationUser()
            {
                UserName = "Administrator",
                Email = "admin@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            var createAdminResult = SeedUser(administratorUser);
            if (createAdminResult.Succeeded)
            {
                ApplicationUser savedAdminUser = _userManager.FindByNameAsync(administratorUser.UserName).Result;
                AddUserToRole(savedAdminUser, "Admin");
            }

            _logger.LogInformation("Finished seeding data");
        }

        private IdentityResult AddUserToRole(ApplicationUser user, string roleName)
        {
            if (!_userManager.IsInRoleAsync(user, roleName).Result)
                return _userManager.AddToRoleAsync(user, roleName).Result;
            else
                return IdentityResult.Success;

        }

        private IdentityResult SeedUser(ApplicationUser user, string password = "")
        {
            string usersPassword = password;
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                if (string.IsNullOrEmpty(usersPassword))
                    usersPassword = GenerateRandomPassword();

                IdentityResult result = _userManager.CreateAsync(user, usersPassword).Result;
                if (result.Succeeded)
                    _logger.LogInformation($"Password for {user.UserName} is: {usersPassword}");
                return result;
            }
            else
                return IdentityResult.Success;
        }

        private IdentityResult SeedRole(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
                return _roleManager.CreateAsync(new IdentityRole(roleName)).Result;
            else
                return IdentityResult.Success;
        }

        private string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 12,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",
            "abcdefghijkmnopqrstuvwxyz",
            "0123456789",
            "!@$?_-"
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
