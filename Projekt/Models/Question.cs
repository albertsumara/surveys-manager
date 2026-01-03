using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Question
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(200)]
        public string Content { get; set; } = string.Empty;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; } = null!;

    }

    

}



