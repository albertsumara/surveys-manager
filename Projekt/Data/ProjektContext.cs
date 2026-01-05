using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ProjektContext
        : IdentityDbContext<ApplicationUser>
    {
        public ProjektContext(DbContextOptions<ProjektContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<SurveyResults> SurveyResults { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

    }
}
