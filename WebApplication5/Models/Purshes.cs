using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static WebApplication5.Data.AppDBContext;

namespace WebApplication5.Models
{
    public class Purshes
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public User User { get; set; }
        public List<GameKeys> GameKeys { get; set; }  =new();


    }

}
