using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Models;
using BloodKoshh.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private UserManager<BloodKoshhUser> _userManager;
        private IWatchListRepository _watchlistRepository;
        //private IClientNotification _clientNotification;
        public WatchlistController(IWatchListRepository watchlistRepository,
                                    UserManager<BloodKoshhUser> userManager
                                    )
        {
            _watchlistRepository = watchlistRepository;
            _userManager = userManager;
            //_clientNotification = clientNotification;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var watchlists = _watchlistRepository.GetUserWatchlist(userId);

            return View(watchlists);
        }

        public IActionResult New(int eventid)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var watchlist = new Watchlist
            {
                EventId = eventid,
                UserId = userId
            };

            _watchlistRepository.Create(watchlist);
           // _clientNotification.AddSweetNotification("Success", "Event has been added to watchlist", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var watchlist = _watchlistRepository.GetWatchlist(id);

            _watchlistRepository.Remove(watchlist);
           // _clientNotification.AddSweetNotification("Success", "Event has been removed from watchlist", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

    }
}
