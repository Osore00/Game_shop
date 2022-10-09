using WebApplication5.Models;

namespace WebApplication5.Abstract
{
	public interface IGameRepository
	{
        IEnumerable<Game> Games { get; }
        IEnumerable<Game> CategoryGames { get; }
        Game GetGameById(int id);
    }
}
