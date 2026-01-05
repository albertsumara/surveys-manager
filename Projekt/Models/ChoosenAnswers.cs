using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class ChoosenAnswers
    {

        public int Id { get; set; }


        //public int QuestionId { get; set; }

        //[ForeignKey("Answer")]
        public int AnswerId { get; set; }
        //public Answer Answer { get; set; } = null!;


        //[ForeignKey("Question")]
        public int QuestionId { get; set; }
        //public Question Question { get; set; } = null!;

    }
}
