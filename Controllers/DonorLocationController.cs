using BloodKoshh.Data;
using BloodKoshh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Controllers
{
    public class DonorLocationController : Controller
    {
        private readonly BloodKoshhContext _context;

        public DonorLocationController(BloodKoshhContext context)
        {
            _context = context;
        }

        // GET: VendorLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonorLocation.ToListAsync());
        }

        // GET: VendorLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorLocation = await _context.DonorLocation
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (donorLocation == null)
            {
                return NotFound();
            }

            return View(donorLocation);
        }

        // GET: VendorLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,LocationName,Latitude,Longitude")] DonorLocation donorLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donorLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donorLocation);
        }

        // GET: VendorLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorLocation = await _context.DonorLocation.FindAsync(id);
            if (donorLocation == null)
            {
                return NotFound();
            }
            return View(donorLocation);
        }

        // POST: VendorLocations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationId,LocationName,Latitude,Longitude")] DonorLocation donorLocation)
        {
            if (id != donorLocation.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donorLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorLocationExists(donorLocation.LocationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donorLocation);
        }

        // GET: VendorLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorLocation = await _context.DonorLocation
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (donorLocation == null)
            {
                return NotFound();
            }

            return View(donorLocation);
        }

        // POST: VendorLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donorLocation = await _context.DonorLocation.FindAsync(id);
            _context.DonorLocation.Remove(donorLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorLocationExists(int id)
        {
            return _context.DonorLocation.Any(e => e.LocationId == id);
        }
    }
}
