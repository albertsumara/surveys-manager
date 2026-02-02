using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Linq;
using System.Text.Json;


namespace Projekt.Controllers
{
    public class SurveyResultsController : BaseController
    {

        private int[] completedSurveys;

        public SurveyResultsController(UserManager<ApplicationUser> userManager, SurveyManagerContext context) : base(userManager, context)
        {

            completedSurveys = CompletedSurveys().ToArray();

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SurveyStatsChooser()
        {

            return View();

        }


        [HttpGet]
        public IActionResult ListSurveysCompleted()
        {

            var completedSurveys = _context.SurveyResults
                .Select(sr => sr.SurveyId)
                .Distinct()
                .ToList();

            var surveys = _context.Surveys
                .Where(s => completedSurveys.Contains(s.Id))
                .Select(s => new { s.Id, s.Title })
                .ToList();

            return Json(surveys);

        }

        [HttpGet]
        public IActionResult ListSurveys()
        {

            var surveys = _context.Surveys
                .Select(s => new { s.Id, s.Title })
                .ToList();
            return Json(surveys);

        }

        [HttpPost]
        public IActionResult Results([FromBody] SurveyResults surveyResults)
        {


            string? UserId = _userManager.GetUserId(User);

            if (UserId == null)
            {
                return BadRequest();
            }

            surveyResults.UserId = UserId;

            //Console.WriteLine("obiekt: "surveyResults);
            _context.SurveyResults.Add(surveyResults);
            _context.SaveChanges();

            return Ok(new { success = true });
        }

        [HttpGet]
        public IActionResult GetResults(int? surveyId)
        {
            var surveyResults = _context.SurveyResults
                .Where(s => s.SurveyId == surveyId)
                .OrderBy(a => a.Id)
                .Include(s => s.ChoosenAnswers)
                .ToList();

            if (surveyResults.Count == 0)
                return NotFound();

            return Json(surveyResults);
        }


        [HttpGet]
        public IActionResult SurveyStats()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStats(int surveyId)
        {

            int ? cityId = HttpContext.Session.GetInt32("CityId");
            int ? ageFrom = HttpContext.Session.GetInt32("AgeFrom");
            int ? ageTo = HttpContext.Session.GetInt32("AgeTo");

            Console.WriteLine("cityid: " + cityId);
            Console.WriteLine("ageFrom: " + ageFrom);
            Console.WriteLine("ageTo: " + ageTo);

            if (surveyId == 0)
            {
                return BadRequest();
            }

            IQueryable<ApplicationUser> userFilter = _context.Users;

            if (ageFrom.HasValue)
                userFilter = userFilter.Where(u => u.Age >= ageFrom.Value);

            if (ageTo.HasValue)
                userFilter = userFilter.Where(u => u.Age <= ageTo.Value);

            if (cityId.HasValue && cityId.Value != 0)
            {

                userFilter = userFilter
                    .Where(u => u.Town == (ApplicationUser.Miasta)cityId.Value);

            }

            var userResult = userFilter
                .Select(u => u.Id)
                .ToList();

            var surveyResults = _context.SurveyResults
                .Where(sr => sr.SurveyId == surveyId && userResult.Contains(sr.UserId))
                .SelectMany(sr => sr.ChoosenAnswers)
                .ToList();

            var stats = surveyResults
                .GroupBy(ca => new { ca.QuestionId, ca.AnswerId })
                .Select(g => new { g.Key.QuestionId, g.Key.AnswerId, Count = g.Count() })
                .ToList()
                .GroupBy(x => x.QuestionId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(x => x.AnswerId, x => x.Count)
                );

            return Json(stats);
        }

        [HttpGet]
        public IActionResult IsCompleted(int surveyId)
        {
            Console.WriteLine("id: " + surveyId);
            return Ok(completedSurveys.Contains(surveyId));

        }

        public List<int> CompletedSurveys()
        {

            var completedSurveys = _context.SurveyResults
                .Select(sr => sr.SurveyId)
                .Distinct()
                .ToList();

            Console.WriteLine("test: " + string.Join(", ", completedSurveys));

            return completedSurveys;

        }

        [HttpGet]
        public IActionResult GetCities()
        {

            var cities = Enum.GetValues(typeof(ApplicationUser.Miasta))
                .Cast<ApplicationUser.Miasta>()
                .Select(c => new
                {
                    Id = (int)c,
                    Name = c.ToString()
                });

            return Json(cities);

        }

        [HttpGet]
        public IActionResult SurveyStatsFilter()
        {

            return View();

        }


        [HttpPost]
        public IActionResult SaveChoosenFilters([FromBody] JsonElement Filters )
        {

            Console.WriteLine("przed zapisem w sesji \ncity: " + Filters.GetProperty("cityId"));
            Console.WriteLine("ageFrom: " + Filters.GetProperty("ageFrom"));
            Console.WriteLine("ageTo: " + Filters.GetProperty("ageTo"));

            HttpContext.Session.SetInt32("CityId", Filters.GetProperty("cityId").GetInt32());
            HttpContext.Session.SetInt32("AgeFrom", Filters.GetProperty("ageFrom").GetInt32());
            HttpContext.Session.SetInt32("AgeTo", Filters.GetProperty("ageTo").GetInt32());


            return Ok(new { success = true });
        }

    }
}
