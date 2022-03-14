﻿using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloodKosh.Controllers
{
    public class DonorController : Controller
    {
        private readonly IDonorProvider _iDonorProvider;
        private BloodKoshhContext _context;

        public DonorController(IDonorProvider iDonorProvider, BloodKoshhContext context)
        {
            _iDonorProvider = iDonorProvider;
            _context = context;

        }
        public IActionResult Index(string searchText = "")
        {
            DonorViewModel donor = new DonorViewModel();
            if (searchText != "" && searchText != null)
            {
                donor.DonorList = (from s in _context.Donors
                                         where s.FirstName.Contains(searchText)
                                         select new DonorViewModel
                                         {
                                             Donor_id = s.Donor_id,
                                             FirstName = s.FirstName,
                                             LastName = s.LastName,
                                             MiddleName = s.MiddleName,
                                             Email = s.Email,
                                             Address = s.Address,
                                         }).ToList();
            }
            else
                donor = _iDonorProvider.GetList();
            return View(donor);
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int? id)
        {
            var DistrictList = _context.Districts.ToList();
            List<SelectListItem> dist = new List<SelectListItem>();
            foreach (var item in DistrictList)
            {
                string data1 = item.Districts;
                SelectListItem items = new SelectListItem { Value = data1, Text = data1 };
                dist.Add(items);
            }
            ViewBag.Dist = dist;
            var AddressList = _context.Address.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.Name;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            DonorViewModel donor = new DonorViewModel();
            if (id.HasValue)
            {
                donor = _iDonorProvider.GetById(id.Value);
            }
            return PartialView(donor);
            ViewBag.Gender = _context.Genders.ToList();
        }
        [HttpPost]
        public IActionResult CreateOrEdit(DonorViewModel model)
        {
            try
            {
                _iDonorProvider.SaveDonor(model);
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
            _iDonorProvider.DeleteDonor(id);
            return RedirectToAction("Index");
        }
        public ActionResult DonorSearch(string val)
        {
            DonorViewModel model = new DonorViewModel();

            model.DonorList = (from s in _context.Donors
                               where s.FirstName.Contains(val)
                                  select new DonorViewModel
                                  {
                                      Donor_id = s.Donor_id,
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
            var users = (from user in this._context.Donors
                         where user.FirstName.StartsWith(prefix)
                         select new
                         {
                             label = user.FirstName,
                             val = user.Donor_id
                         }).ToList();

            return Json(users);
        }
    }
}