using BloodKosh.Data;
using BloodKosh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventProvider _iEventProvider;
        private BloodKoshContext _context;

        public EventController(IEventProvider iEventProvider, BloodKoshContext context)
        {
            _iEventProvider = iEventProvider;
            _context = context;

        }
        public IActionResult Index(string searchText = "")
        {
            EventViewModel events = new EventViewModel();
            if (searchText != "" && searchText != null)
            {
                events.EventList = (from s in _context.Events
                                     where s.EventName.Contains(searchText)
                                     select new EventViewModel
                                     {
                                         EventId = s.EventId,
                                         EventDate = s.EventDate,
                                         EventName = s.EventName,
                                         Location = s.Location
                                     }).ToList();
            }
            else
                events = _iEventProvider.GetList();
            return View(events);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            EventViewModel model = new EventViewModel();
            if (id.HasValue)
            {
                model = _iEventProvider.GetById(id.Value);
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(EventViewModel model)
        {
            try
            {
                _iEventProvider.SaveEvent(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Delete(int id)
        {
            _iEventProvider.DeleteEvent(id);
            return RedirectToAction("Index");
        }
        public ActionResult EventSearch(string val)
        {
            EventViewModel model = new EventViewModel();

            model.EventList = (from s in _context.Events
                                where s.EventName.Contains(val)
                                select new EventViewModel
                                {
                                    EventId = s.EventId,
                                    EventName = s.EventName,
                                    EventDate = s.EventDate,
                                    Location = s.Location
                                }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var events = (from user in this._context.Events
                         where user.EventName.StartsWith(prefix)
                         select new
                         {
                             label = user.EventName,
                             val = user.EventId
                         }).ToList();

            return Json(events);
        }
    }
}
