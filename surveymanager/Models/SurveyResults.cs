using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class SurveyResults
    {

        public int Id { get; set; }

        //[ForeignKey("Survey")]
        public int SurveyId { get; set; }

        public string UserId { get; set; }
        //public Survey Survey { get; set; } = null!;

        public ICollection<ChoosenAnswers> ChoosenAnswers { get; set; } = new List<ChoosenAnswers>();


    }
}
