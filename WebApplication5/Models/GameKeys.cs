namespace WebApplication5.Models
{
    public class GameKeys
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Game Game { get; set; }
        public Purshes? Purshes { get; set; }
        public bool Realized { get; set; }
    }
}
