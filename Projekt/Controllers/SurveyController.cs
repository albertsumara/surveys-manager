using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class SurveyController : BaseController
    {

        public SurveyController(ProjektContext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult SurveyCreator()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult CreateSurveyTitle()
        //{
        //    return View("SurveyCreator");
        //}

        [HttpPost]
        public IActionResult CreateSurveyTitle(Survey model)
        {
            _context.Surveys.Add(model);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("SurveyId", model.Id);
            TempData["SurveyId"] = model.Id;

            return RedirectToAction("CreateSurveyQuestion");

        }

        [HttpGet]
        public IActionResult CreateSurveyQuestion()
        {
            return View("SurveyQuestionCreator");
        }

        [HttpPost]
        public IActionResult CreateSurveyQuestion(String content)
        {

            var question = new Question
            {
                Content = content,
                SurveyId = (int)HttpContext.Session.GetInt32("SurveyId")

            };

            

            _context.Questions.Add(question);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("QuestionId", question.Id);

            return View("SurveyAnswerCreator");

        }

        [HttpGet]
        public IActionResult CreateSurveyAnswer()
        {
            return View("SurveyAnswerCreator");
        }

        [HttpPost]
        public IActionResult CreateSurveyAnswer(String content)
        {

            var answer = new Answer
            {
                Content = content,
                QuestionId = (int)HttpContext.Session.GetInt32("QuestionId")

            };


            _context.Answers.Add(answer);
            _context.SaveChanges();

            return View("SurveyAnswerCreator");

        }

    }
}
