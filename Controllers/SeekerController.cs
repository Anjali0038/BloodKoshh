using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Controllers
{
    public class SeekerController : Controller
    {
        private readonly ISeekerProvider _iSeekerProvider;
        private BloodKoshhContext _context;

        public SeekerController(ISeekerProvider iSeekerProvider, BloodKoshhContext context)
        {
            _iSeekerProvider = iSeekerProvider;
            _context = context;

        }
        public IActionResult Index(string searchText = "")
        {
            SeekerViewModel seeker = new SeekerViewModel();
            if (searchText != "" && searchText != null)
            {
                seeker.SeekerList = (from s in _context.Seekers
                                   where s.FirstName.Contains(searchText)
                                   select new SeekerViewModel
                                   {
                                       Seeker_Id = s.Seeker_Id,
                                       FirstName = s.FirstName,
                                       LastName = s.LastName,
                                       MiddleName = s.MiddleName,
                                       Email = s.Email,
                                       Address = s.Address,
                                   }).ToList();
            }
            else
                seeker = _iSeekerProvider.GetList();
            //ViewBag.Gender = _context.Genders.ToList();
            return View(seeker);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            //ViewBag.Gender = _context.Genders.ToList();
            SeekerViewModel emp = new SeekerViewModel();
            if (id.HasValue)
            {
                emp = _iSeekerProvider.GetById(id.Value);
            }
            return PartialView(emp);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(SeekerViewModel model)
        {
            try
            {
                _iSeekerProvider.SaveSeeker(model);
                TempData["Success"] = "Success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Delete(int id)
        {
            _iSeekerProvider.DeleteSeeker(id);
            return RedirectToAction("Index");
        }
        public ActionResult SeekerSearch(string val)
        {
            SeekerViewModel model = new SeekerViewModel();

            model.SeekerList = (from s in _context.Seekers
                                where s.FirstName.Contains(val)
                               select new SeekerViewModel
                               {
                                   Seeker_Id = s.Seeker_Id,
                                   FirstName = s.FirstName,
                                   LastName = s.LastName,
                                   MiddleName = s.MiddleName,
                                   Email = s.Email,
                                   Address = s.Address,
                               }).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var users = (from user in this._context.Seekers
                         where user.FirstName.StartsWith(prefix)
                         select new
                         {
                             label = user.FirstName,
                             val = user.Seeker_Id
                         }).ToList();

            return Json(users);
        }
    }
}
