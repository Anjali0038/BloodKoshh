using BloodKoshh.Data;
using BloodKoshh.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public class WatchListRepository : IWatchListRepository
    {
        private BloodKoshhContext _context;
        public WatchListRepository(BloodKoshhContext context)
        {
            _context = context;
        }

        public bool CheckIfAlreadyExists(string userId, int eventid)
        {
            return _context.Watchlists.Any(w => w.UserId.Equals(userId) && w.EventId == eventid);
        }

        public void Create(Watchlist watchlist)
        {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
        }

        public List<Watchlist> GetUserWatchlist(string userId)
        {
            return _context.Watchlists
                            .Include(w => w.Event)
                            .Where(w => w.UserId.Equals(userId))
                            .ToList();
        }

        public Watchlist GetWatchlist(int Id)
        {
            return _context.Watchlists.FirstOrDefault(w => w.Id == Id);
        }

        public List<Watchlist> GetWatchlistFromEventId(int eventid)
        {
            return _context.Watchlists.Where(w => w.EventId == eventid).ToList();
        }

        public void Remove(Watchlist watchlist)
        {
            _context.Watchlists.Remove(watchlist);
            _context.SaveChanges();
        }
    }
}
