using Microsoft.AspNetCore.Identity;
using NetKubernet.Models;

namespace NetKubernet.Data;

public class LoadDatabase
{

    public static async Task InsertData(AppDbContext context, UserManager<User> userManager)
    {

        if (!userManager.Users.Any())
        {
            var user = new User
            {
                Name = "Julian",
                Surname = "Abregovich",
                Email = "julianabregovich@gmail.com",
                UserName = "juli.abre",
                Phone = "1136379245"
            };

            await userManager.CreateAsync(user, "PasswordParalelepipedo123$");
        }

        if (!context.Properties!.Any())
        {
            context.Properties!.AddRange(
                new Property
                {
                    Name = "Beach House",
                    Adress = "Sun avenue 32",
                    Price = 4500M,
                    DateCreated = DateTime.Now
                },
                new Property
                {
                    Name = "Regular House",
                    Adress = "Dogue coin street 4542",
                    Price = 3500M,
                    DateCreated = DateTime.Now
                }
            );
        }
        context.SaveChanges();
    }
}