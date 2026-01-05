using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;

using Projekt.Models;


namespace Projekt.Controllers
{
    public class SurveyResultsController : BaseController
    {

        public SurveyResultsController(UserManager<ApplicationUser> userManager, ProjektContext context) : base(userManager, context)
        {
        }
        public IActionResult Index()
        {
            return View();
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
        public IActionResult GetResults(int surveyId)
        {


            string? userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest();
            }

            var surveyResult = _context.SurveyResults
                .Where(s => s.SurveyId == surveyId && s.UserId == userId)
                .OrderBy(a => a.Id)
                .Include(s => s.ChoosenAnswers)
                .FirstOrDefault();

            if (surveyResult == null)
            {

                return NotFound();
            }

            return Json(surveyResult);


        }

    }
}
