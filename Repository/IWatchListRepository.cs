using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface IWatchListRepository
    {
        Watchlist GetWatchlist(int Id);
        void Create(Watchlist watchlist);
        List<Watchlist> GetUserWatchlist(string userId);
        void Remove(Watchlist watchlist);
        bool CheckIfAlreadyExists(string userId, int eventid);
        List<Watchlist> GetWatchlistFromEventId(int eventid);
    }
}
