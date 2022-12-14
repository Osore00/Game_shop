using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Game> Games { get; set; }

        public Category()
        {
            Games = new List<Game>();
        }
    }
}
