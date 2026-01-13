using Microsoft.AspNetCore.Identity;
using Projekt.Models;

namespace Projekt.Data.Initializers;

public static class UserInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // ADMIN
        await CreateUserIfNotExists(
            userManager,
            email: "admin@admin.admin",
            password: "Admin123!",
            name: "Admin",
            surname: "Admin",
            age: 35,
            town: ApplicationUser.Miasta.Kraków,
            role: "Admin"
        );

        // USERS
        var users = new List<(string Email, string Name, string Surname, int Age, ApplicationUser.Miasta Town)>
        {
            ("jan.kowalski@test.pl", "Jan", "Kowalski", 22, ApplicationUser.Miasta.Warszawa),
            ("anna.nowak@test.pl", "Anna", "Nowak", 28, ApplicationUser.Miasta.Kraków),
            ("piotr.zielinski@test.pl", "Piotr", "Zieliński", 35, ApplicationUser.Miasta.Gdańsk),
            ("kasia.lewandowska@test.pl", "Kasia", "Lewandowska", 41, ApplicationUser.Miasta.Poznań),
            ("tomasz.mazur@test.pl", "Tomasz", "Mazur", 19, ApplicationUser.Miasta.Tarnów),
            ("ola.kaczmarek@test.pl", "Ola", "Kaczmarek", 55, ApplicationUser.Miasta.Warszawa),
            ("pawel.dabrowski@test.pl", "Paweł", "Dąbrowski", 67, ApplicationUser.Miasta.Kraków),
            ("magda.wojcik@test.pl", "Magda", "Wójcik", 30, ApplicationUser.Miasta.Gdańsk),
            ("lukasz.pawlak@test.pl", "Łukasz", "Pawlak", 24, ApplicationUser.Miasta.Poznań),
            ("monika.krupa@test.pl", "Monika", "Krupa", 45, ApplicationUser.Miasta.Tarnów)
        };

        foreach (var u in users)
        {
            await CreateUserIfNotExists(
                userManager,
                email: u.Email,
                password: "User123!",
                name: u.Name,
                surname: u.Surname,
                age: u.Age,
                town: u.Town,
                role: "User"
            );
        }
    }

    private static async Task CreateUserIfNotExists(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string name,
        string surname,
        int age,
        ApplicationUser.Miasta town,
        string role)
    {
        if (await userManager.FindByEmailAsync(email) != null)
            return;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            Name = name,
            Surname = surname,
            Age = age,
            Town = town
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new Exception(
                $"Nie udało się utworzyć użytkownika {email}: " +
                string.Join(", ", result.Errors.Select(e => e.Description))
            );
        }

        await userManager.AddToRoleAsync(user, role);
    }
}
