using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Open_School_Library.Data;
using Open_School_Library.Data.Entities;
using Open_School_Library.Models.SettingViewModel;

namespace Open_School_Library.Controllers
{
    public class SettingsController : Controller
    {
        private readonly LibraryContext _context;

        public SettingsController(LibraryContext context)
        {
            _context = context;    
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {

            var settings =
            _context.Settings
            .Select(r => new SettingIndexViewModel
            {
                SettingID = r.SettingID,
                FineAmountPerDay = r.FineAmountPerDay,
                CheckoutDurationInDays = r.CheckoutDurationInDays

            }).FirstOrDefault();

            if (settings == null)
            {
                return NotFound();
            }

            return View(settings);
        }

        // POST: Settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int SettingID, [Bind("SettingID, FineAmountPerDay, CheckoutDurationInDays")] Setting setting)
        {
            if (SettingID != setting.SettingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.SettingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.SettingID == id);
        }
    }
}
