using WebApplication5.Models;

namespace WebApplication5.Abstract
{
	public interface IGameRepository
	{
       List<Game> Games { get; }
       List<Game> CategoryGames { get; }
        Game GetGameById(int id);
    }
}
