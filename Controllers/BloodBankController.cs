using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloodKosh.Controllers
{
    //[Authorize(Roles = "BloodBank,Admin")]
    public class BloodBankController : Controller
    {
        private readonly IBloodBankProvider _iBloodBankProvider;
        private BloodKoshhContext _context;

        public BloodBankController(IBloodBankProvider iBloodBankProvider, BloodKoshhContext context)
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
                                   where s.BloodBankName.Contains(searchText) && s.Location.Contains(searchText)
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
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
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
        [HttpGet]
        public IActionResult ApprovedBloodBanks()
        {
            var data = _iBloodBankProvider.GetApprovedBanks();
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
            BloodBankViewModel bank = new BloodBankViewModel();
            if (id.HasValue)
            {
                bank = _iBloodBankProvider.GetById(id.Value);
            }
            return View(bank);
        }
        [HttpPost]
        public IActionResult Approve(BloodBankViewModel model)
        {
            try
            {
                _iBloodBankProvider.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        public ActionResult BloodBankSearch(string val)
        {
            BloodBankViewModel model = new BloodBankViewModel();

            model.BankList = (from s in _context.BloodBanks
                              where s.Location.Contains(val)
                              select new BloodBankViewModel
                              {
                                  Id = s.Id,
                                  BloodBankName = s.BloodBankName,
                                  Location = s.Location,
                                  Phone_No = s.Phone_No,
                              }).ToList();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var banks = (from bank in this._context.BloodBanks
                         where bank.Location.StartsWith(prefix) 
                         select new
                         {
                             label = bank.Location,
                             val = bank.Id
                         }).ToList();

            return Json(banks);
        }
    }

}
