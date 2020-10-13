using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Web.AppData
{
    public static class Seed
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            //CreateInitialCountries(serviceProvider);

            await CreateUser(serviceProvider);

        }

        private static void CreateInitialCountries(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ZadatakContext>();
            context.Database.EnsureCreated();
            if (!context.Drzave.Any())
            {
                using (var reader = new StreamReader("/Drzave.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        context.Drzave.Add(new Drzava
                        {
                            Naziv = values[0],
                            Sifra = values[1]
                        });
                    }
                }
                context.SaveChanges();
            }
        }

        private static async Task CreateUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var roleExists = await roleManager.RoleExistsAsync("admin");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }


            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = new IdentityUser
            {
                UserName = "sencar.ana@gmail.com",
                Email = "sencar.ana@gmail.com",
                EmailConfirmed = true,
            };
            var createdUser = await userManager.CreateAsync(user, "Pass123");
            if (createdUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }


            //var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            //var user = await userManager.FindByNameAsync("Admin");
            //if (user == null)
            //{
            //    user = new IdentityUser
            //    {
            //        UserName = "Admin",
            //        EmailConfirmed = true,
            //    };
            //    IdentityResult ir = await userManager.CreateAsync(user, "Pass123");
            //    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //    if (!await roleManager.RoleExistsAsync("admin"))
            //    {
            //        await roleManager.CreateAsync(new IdentityRole("admin"));
            //    }
            //    await userManager.AddToRoleAsync(user, "admin");
            //}
        }
    }
}
