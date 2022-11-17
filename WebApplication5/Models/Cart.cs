namespace WebApplication5.Models
{

    public class CartLine
    {
        public Game Game { get; set; } = new();
        public int Quantity { get; set; }
    }
}
