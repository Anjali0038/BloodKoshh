using BloodKosh.Data;
using BloodKosh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BloodBankController : Controller
    {
        private readonly IBloodBankProvider _iBloodBankProvider;
        private BloodKoshContext _context;

        public BloodBankController(IBloodBankProvider iBloodBankProvider, BloodKoshContext context)
        {
            _iBloodBankProvider = iBloodBankProvider;
            _context = context;

        }
        [AllowAnonymous]
        public IActionResult Index(string searchText = "")
        {
            BloodBankViewModel bank = new BloodBankViewModel();
            if (searchText != "" && searchText != null)
            {
                bank.BankList = (from s in _context.BloodBanks
                                   where s.BloodBankName.Contains(searchText) && bank.Location.Contains(searchText)
                                 select new BloodBankViewModel
                                   {
                                       Id = s.Id,
                                       BloodBankName = s.BloodBankName,
                                       Location = s.Location,
                                       Phone_No = s.Phone_No,
                                   }).ToList();
            }
            else
                bank = _iBloodBankProvider.GetList();
            //ViewBag.Gender = _context.Genders.ToList();
            return View(bank);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            BloodBankViewModel bank = new BloodBankViewModel();
            if (id.HasValue)
            {
                bank = _iBloodBankProvider.GetById(id.Value);
            }
            return PartialView(bank);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(BloodBankViewModel model)
        {
            try
            {
                _iBloodBankProvider.SaveBloodBank(model);
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
            _iBloodBankProvider.DeleteBloodBank(id);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult BloodBankSearch(string val)
        {
            BloodBankViewModel model = new BloodBankViewModel();

            model.BankList = (from s in _context.BloodBanks
                              where s.BloodBankName.Contains(val)
                              select new BloodBankViewModel
                              {
                                  Id = s.Id,
                                  BloodBankName = s.BloodBankName,
                                  Location = s.Location,
                                  Phone_No = s.Phone_No,
                              }).ToList();
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var banks = (from bank in this._context.BloodBanks
                         where bank.BloodBankName.StartsWith(prefix) && bank.Location.StartsWith(prefix)
                         select new
                         {
                             label = bank.BloodBankName,
                             val = bank.Id
                         }).ToList();

            return Json(banks);
        }
    }

}
