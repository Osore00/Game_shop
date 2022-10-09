namespace WebApplication5.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Date_of_public { get; set; }
        public string LongDesc { get; set; }
        public uint Price { get; set; }
        public string Photos { get; set; }
        public bool Available { get; set; }
        public List<Category> Catigories { get; set; }

    }
}
