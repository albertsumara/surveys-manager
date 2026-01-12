using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Surname { get; set; }

        [Range(18, 99)]
        public int? Age { get; set; }

        public Miasta Town { get; set; }

        public enum Miasta
        {
            Kraków = 1,
            Warszawa = 2,
            Gdańsk = 3,
            Tarnów = 4,
            Poznań = 5
        }
    }
}
