using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloodKosh.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventProvider _iEventProvider;
        private BloodKoshhContext _context;

        public EventController(IEventProvider iEventProvider, BloodKoshhContext context)
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
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            EventViewModel model = new EventViewModel();
            if (id.HasValue)
            {
                model = _iEventProvider.GetById(id.Value);
            }
            return View(model);
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
        [HttpGet]
        public IActionResult ApprovedEvents()
        {
            var data = _iEventProvider.GetApprovedEvents();
            return View(data);
        }
        [HttpGet]
        public IActionResult Approve(int? id)
        {
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            EventViewModel model = new EventViewModel();
            if (id.HasValue)
            {
                model = _iEventProvider.GetById(id.Value);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Approve(EventViewModel model)
        {
            try
            {
                _iEventProvider.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Search(string val)
        {
            EventViewModel model = new EventViewModel();

            model.EventList = (from s in _context.Events
                                  where s.Location.Contains(val)
                                  select new EventViewModel
                                  {
                                      EventId = s.EventId,
                                      EventName = s.EventName,
                                      Location = s.Location,
                                      PhoneNo = s.PhoneNo
                                  }).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var users = (from user in this._context.Events
                         where user.Location.StartsWith(prefix)
                         select new
                         {
                             label = user.Location,
                             val = user.EventId
                         }).ToList();
            return Json(users);
        }
    }
}
