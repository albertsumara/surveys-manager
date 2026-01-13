using Projekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Projekt.Data.Initializers;

public static class SurveyResultsInitializer
{
    public static async Task InitializeAsync(IServiceProvider services, Random random)
    {
        var context = services.GetRequiredService<ProjektContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (await context.SurveyResults.AnyAsync())
            return;

        var users = await userManager.Users.ToListAsync();
        var surveys = await context.Surveys
            .Include(s => s.Questions)
                .ThenInclude(q => q.Answers)
            .ToListAsync();

        var results = new List<SurveyResults>();

        foreach (var survey in surveys)
        {
            foreach (var user in users)
            {
                var surveyResult = new SurveyResults
                {
                    SurveyId = survey.Id,
                    UserId = user.Id
                };

                foreach (var question in survey.Questions)
                {
                    if (question.Answers.Any())
                    {
                        var answer = question.Answers.ElementAt(random.Next(question.Answers.Count));

                        surveyResult.ChoosenAnswers.Add(new ChoosenAnswers
                        {
                            QuestionId = question.Id,
                            AnswerId = answer.Id
                        });
                    }
                }

                results.Add(surveyResult);
            }
        }

        context.SurveyResults.AddRange(results);
        await context.SaveChangesAsync();
    }
}