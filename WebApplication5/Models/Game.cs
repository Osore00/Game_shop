using System.ComponentModel.DataAnnotations;
using static WebApplication5.Models.Purshes;

namespace WebApplication5.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Не вказана назва Ігри")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжинна повинна бути від 3 до 30 символів")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Не вказана назва Розробника")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Довжинна повинна бути від 3 до 30 символів")]
        public string? Producer { get; set; }
        [Required(ErrorMessage = "Не вказана дата Публікації")]
        public string? Date_of_public { get; set; }
        public string? LongDesc { get; set; }
        [Required(ErrorMessage = "Не вказана Ціна")]
        [Range (0, 10000, ErrorMessage = "Недопустима ціна")]
        public uint? Price { get; set; }
        public string? Photos { get; set; }
        public List<Category>? Categories { get; set; } = new();

        public List<GameKeys> GameKeys { get; set; } = new();

    }
}
