using BloodKoshh.Data;
using BloodKoshh.Models;
using BloodKoshh.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private BloodKoshhContext _context;
        
        public HomeController(ILogger<HomeController> logger, BloodKoshhContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            ViewData["donorinfoid"] = new SelectList(_context.Donors, "Donor_id", "BloodGroup");
            ViewData["LocationId"] = new SelectList(_context.DonorLocation, "LocationId", "LocationName");

            return View();
        }
        [HttpPost]
        public IActionResult SearchNearestLocation(UserViewModel user)
        {
            var address = _context.DonorLocation.Where(x => x.LocationId == user.LocationId).FirstOrDefault();
            var donor = _context.Donors.Where(x => x.Donor_id == user.DonorId).ToList();
            var allVendors = _context.DonorLocation.ToList();
            ViewData["donorinfoid"] = new SelectList(_context.Donors, "Donor_id", "BloodGroup");
            List<Locationinf> locationinfo = new List<Locationinf>();
            foreach (var item in allVendors)
            {
                NearestNeighbourProvider nearestNeighbour = new NearestNeighbourProvider();
                var pointer = nearestNeighbour.getDistanceFromLatLonInKm(address.Latitude, address.Longitude, item.Latitude, item.Longitude);
                locationinfo.Add(new Locationinf
                {
                    LocationId = item.LocationId,
                    address = item.LocationName,
                    pointer = pointer
                    //DonorName = item.Donor.FirstName,
                    //BloodGroup=item.Donor.BloodGroup
                });
            }

            locationinfo = locationinfo.OrderBy(x => x.pointer).Take(5).ToList();
            ViewData["AddressId"] = new SelectList(_context.DonorLocation, "LocationId", "LocationName");
            return View(locationinfo);

            //var address = _context.DonorLocation.Where(x => x.LocationId == user.LocationId).FirstOrDefault();
            //var donor = _context.Donors.Where(x => x.Donor_id == user.DonorId).ToList();
            //var allVendors = _context.Donors.ToList();
            //ViewData["donorinfoid"] = new SelectList(_context.Donors, "Donor_id", "BloodGroup");
            //List<Locationinf> locationinfo = new List<Locationinf>();
            //foreach (var item in allVendors)
            //{
            //    NearestNeighbourProvider nearestNeighbour = new NearestNeighbourProvider();
            //    var pointer = nearestNeighbour.getDistanceFromLatLonInKm(address.Latitude, address.Longitude, item.DonorLocation.Latitude, item.DonorLocation.Longitude);
            //    locationinfo.Add(new Locationinf
            //    {
            //        LocationId = item.DonorLocation.LocationId,
            //        address = item.DonorLocation.LocationName,
            //        DonorName=item.FirstName,
            //        BloodGroup = item.BloodGroup,
            //        pointer = pointer
            //        //DonorName = item.Donor.FirstName,
            //        //BloodGroup=item.Donor.BloodGroup
            //    });
            //}

            //locationinfo = locationinfo.OrderBy(x => x.pointer).Take(5).ToList();
            //ViewData["AddressId"] = new SelectList(_context.DonorLocation, "LocationId", "LocationName");
            //return View(locationinfo);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class Locationinf
    {
        public double pointer { get; set; }
        public string address { get; set; }
        public int LocationId { get; set; }
        public string DonorName { get; set; }
        public string BloodGroup { get; set; }


    }
}
