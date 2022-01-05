using Quiz_back.Models;
using Quiz_back.repositories.interfaces;
using System;
using System.Collections.Generic;


namespace Quiz_back.repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;
        public PlayerRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Player Create(Player player)
        {
            var newPlayer = _context.Players.Add(player);
            _context.SaveChanges();

            return newPlayer.Entity;
        }

        public Player Delete(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
            return player;
        }

        public IEnumerable<Player> ReadAll() => _context.Players;
      
    }
}
