using WebApplication5.Models;

namespace WebApplication5.ViewsModels
{
    public class UserOdrderCart
    {
        public User User { get; set; }
       public List<CartLine> cartLines { set; get; }


    }
}
