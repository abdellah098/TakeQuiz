using Quiz_back.Models;
using System.Collections.Generic;

namespace Quiz_back.repositories.interfaces
{
    public interface IPlayerRepository
    {
        public Player Create(Player player);
        public IEnumerable<Player> ReadAll();
        public Player Delete(Player player);
    }
}
