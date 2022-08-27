using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCCF_MVC_App.Models;

namespace NCCF_MVC_App.Controllers
{
    public class FoodStuffsController : Controller
    {
        private readonly NCCF_DatabaseContext _context;

        public FoodStuffsController(NCCF_DatabaseContext context)
        {
            _context = context;
        }

        // GET: FoodStuffs
        public async Task<IActionResult> Index()
        {
              return _context.FoodStuffs != null ? 
                          View(await _context.FoodStuffs.ToListAsync()) :
                          Problem("Entity set 'NCCF_DatabaseContext.FoodStuffs'  is null.");
        }

        // GET: FoodStuffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodStuffs == null)
            {
                return NotFound();
            }

            var foodStuff = await _context.FoodStuffs
                .FirstOrDefaultAsync(m => m.FoodStuffId == id);
            if (foodStuff == null)
            {
                return NotFound();
            }

            return View(foodStuff);
        }

        // GET: FoodStuffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodStuffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodStuffId,Name,Price,QuantityAvailable,EquipmentId,UnitId,DateAdded,DateUpdated")] FoodStuff foodStuff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodStuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodStuff);
        }

        // GET: FoodStuffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodStuffs == null)
            {
                return NotFound();
            }

            var foodStuff = await _context.FoodStuffs.FindAsync(id);
            if (foodStuff == null)
            {
                return NotFound();
            }
            return View(foodStuff);
        }

        // POST: FoodStuffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodStuffId,Name,Price,QuantityAvailable,EquipmentId,UnitId,DateAdded,DateUpdated")] FoodStuff foodStuff)
        {
            if (id != foodStuff.FoodStuffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodStuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodStuffExists(foodStuff.FoodStuffId))
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
            return View(foodStuff);
        }

        // GET: FoodStuffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodStuffs == null)
            {
                return NotFound();
            }

            var foodStuff = await _context.FoodStuffs
                .FirstOrDefaultAsync(m => m.FoodStuffId == id);
            if (foodStuff == null)
            {
                return NotFound();
            }

            return View(foodStuff);
        }

        // POST: FoodStuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodStuffs == null)
            {
                return Problem("Entity set 'NCCF_DatabaseContext.FoodStuffs'  is null.");
            }
            var foodStuff = await _context.FoodStuffs.FindAsync(id);
            if (foodStuff != null)
            {
                _context.FoodStuffs.Remove(foodStuff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodStuffExists(int id)
        {
          return (_context.FoodStuffs?.Any(e => e.FoodStuffId == id)).GetValueOrDefault();
        }
    }
}
