﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly DataContext _context;

        public LesseesController(DataContext context)
        {
            _context = context;
        }

        // GET: Lessees
        public async Task<IActionResult> Index()
        {
              return _context.Lessee != null ? 
                          View(await _context.Lessee.ToListAsync()) :
                          Problem("Entity set 'DataContext.Lessee'  is null.");
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessee == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,FirstName,LastName,ProfilePhotoUrl,FixedPhone,CellPhone,Address")] Lessee lessee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lessee);
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessee == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee.FindAsync(id);
            if (lessee == null)
            {
                return NotFound();
            }
            return View(lessee);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Document,FirstName,LastName,ProfilePhotoUrl,FixedPhone,CellPhone,Address")] Lessee lessee)
        {
            if (id != lessee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LesseeExists(lessee.Id))
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
            return View(lessee);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessee == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessee == null)
            {
                return Problem("Entity set 'DataContext.Lessee'  is null.");
            }
            var lessee = await _context.Lessee.FindAsync(id);
            if (lessee != null)
            {
                _context.Lessee.Remove(lessee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LesseeExists(int id)
        {
          return (_context.Lessee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
