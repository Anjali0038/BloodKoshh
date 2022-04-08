using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKosh.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace BloodKosh.Controllers
{
    public class DonorController : Controller
    {
        private readonly IDonorProvider _iDonorProvider;
        private BloodKoshhContext _context;
        private readonly IMapper _mapper;


        public DonorController(IDonorProvider iDonorProvider, BloodKoshhContext context, IMapper mapper)
        {
            _iDonorProvider = iDonorProvider;
            _context = context;
            _mapper = mapper;

        }
        [AllowAnonymous]
        public IActionResult Index(string searchText ="")
        {
            DonorViewModel donor = new DonorViewModel();
            if (searchText !="" && searchText != null)
            {
                donor.DonorList = (from s in _context.Donors
                                         where s.Address.Contains(searchText)
                                         select new DonorViewModel
                                         {
                                             Donor_id = s.Donor_id,
                                             FirstName = s.FirstName,
                                             PhoneNo = s.PhoneNo,
                                             Email = s.Email,
                                             Address = s.Address,
                                             BloodGroup = s.BloodGroup
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
                string data1 = item.District;
                SelectListItem items = new SelectListItem { Value = data1, Text = data1 };
                dist.Add(items);
            }
            ViewBag.Dist = dist;
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            DonorViewModel donor = new DonorViewModel();
            if (id.HasValue)
            {
                donor = _iDonorProvider.GetById(id.Value);
            }
            ViewBag.Gender = _context.Genders.ToList();
            return View(donor);
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
        [HttpGet]
        public IActionResult ApprovedDonors()
        {
            var data = _iDonorProvider.GetApprovedDonors();
            return View(data);
        }
        [HttpGet]
        public IActionResult Approve(int? id)
        {
            var DistrictList = _context.Districts.ToList();
            List<SelectListItem> dist = new List<SelectListItem>();
            foreach (var item in DistrictList)
            {
                string data1 = item.District;
                SelectListItem items = new SelectListItem { Value = data1, Text = data1 };
                dist.Add(items);
            }
            ViewBag.Dist = dist;
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            DonorViewModel donor = new DonorViewModel();
            if (id.HasValue)
            {
                donor = _iDonorProvider.GetById(id.Value);
            }
            ViewBag.Gender = _context.Genders.ToList();
            return View(donor);
        }
        [HttpPost]
        public IActionResult Approve(DonorViewModel model)
        {
            try
            {
                _iDonorProvider.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult Request(int? id)
        {
            var DistrictList = _context.Districts.ToList();
            List<SelectListItem> dist = new List<SelectListItem>();
            foreach (var item in DistrictList)
            {
                string data1 = item.District;
                SelectListItem items = new SelectListItem { Value = data1, Text = data1 };
                dist.Add(items);
            }
            ViewBag.Dist = dist;
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            DonorViewModel donor = new DonorViewModel();
            if (id.HasValue)
            {
                donor = _iDonorProvider.GetById(id.Value);
            }
            ViewBag.Gender = _context.Genders.ToList();
            return View(donor);
        }
        [HttpPost]
        public IActionResult Request(DonorViewModel model)
        {
            try
            {
                _iDonorProvider.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Donor donor = new Donor();
            //donor.RequestStatus = model.RequestStatus;
            //_context.Donors.Update(donor);
            //_context.SaveChanges();
            //return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Donate(int? id)
        {
            var DistrictList = _context.Districts.ToList();
            List<SelectListItem> dist = new List<SelectListItem>();
            foreach (var item in DistrictList)
            {
                string data1 = item.District;
                SelectListItem items = new SelectListItem { Value = data1, Text = data1 };
                dist.Add(items);
            }
            ViewBag.Dist = dist;
            var AddressList = _context.DonorLocation.ToList();
            List<SelectListItem> address = new List<SelectListItem>();
            foreach (var item in AddressList)
            {
                string data = item.LocationName;
                SelectListItem items = new SelectListItem { Value = data, Text = data };
                address.Add(items);
            }
            ViewBag.Address = address;
            DonorViewModel donor = new DonorViewModel();
            if (id.HasValue)
            {
                donor = _iDonorProvider.GetById(id.Value);
            }
            ViewBag.Gender = _context.Genders.ToList();
            return View(donor);
        }
        [HttpPost]
        public IActionResult Donate(DonorViewModel model)
        {
            try
            {
                _iDonorProvider.Edit(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        public IActionResult Frequent(DonorViewModel model)
        {
            var data = _iDonorProvider.GetFrequentDonors();
            return PartialView(data);
        }
        [AllowAnonymous]
        public ActionResult Search(string val)
        {
            DonorViewModel model = new DonorViewModel();

            model.DonorList = (from s in _context.Donors
                                  where s.BloodGroup.Contains(val) || s.FirstName.Contains(val)|| s.Address.Contains(val)
                               select new DonorViewModel
                                  {
                                      Donor_id = s.Donor_id,
                                      FirstName = s.FirstName,
                                      Email = s.Email,
                                      PhoneNo = s.PhoneNo,
                                      BloodGroup =s.BloodGroup
                                  }).ToList();
            return PartialView(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var users = (from user in this._context.Donors
                         where user.Address.StartsWith(prefix)
                         select new
                         {
                             label = user.Address,
                             val = user.Donor_id
                         }).ToList();
            return Json(users);
        }
    }
}
