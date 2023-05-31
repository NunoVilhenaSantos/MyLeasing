﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using Serilog;

namespace MyLeasing.Web.Controllers;

public class OwnersController : Controller
{
    private readonly DataContext _context;

    public OwnersController(DataContext context)
    {
        _context = context;
    }

    // GET: Owners
    public async Task<IActionResult> Index()
    {
        return View(await _context.Owners.ToListAsync());
    }

    // GET: Owners/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _context.Owners
            .FirstOrDefaultAsync(m => m.Id == id);

        if (owner == null) return NotFound();

        return View(owner);
    }

    // GET: Owners/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Owners/Create
    // To protect from overposting attacks,
    // enable the specific properties you want to bind to.
    // For more details,
    // see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address")]
        Owner owner)
    {
        if (!ModelState.IsValid) return View(owner);

        _context.Add(owner);

        // await _context.SaveChangesAsync();
        if (!await SaveChangesAsync())
        {
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Owners/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _context.Owners.FindAsync(id);

        if (owner == null) return NotFound();

        return View(owner);
    }

    // POST: Owners/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind(
            "Id,Document,FirstName,LastName,FixedPhone,CellPhone,Address")]
        Owner owner)
    {
        if (id != owner.Id) return NotFound();

        if (!ModelState.IsValid) return View(owner);

        try
        {
            _context.Update(owner);
            // await _context.SaveChangesAsync();
            if (!await SaveChangesAsync())
            {
                Log.Logger.Error(
                    "Error creating owner: {0}, {1}",
                    owner.Id, owner.FullName);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OwnerExists(owner.Id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Owners/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _context.Owners
            .FirstOrDefaultAsync(m => m.Id == id);

        if (owner == null) return NotFound();

        return View(owner);
    }

    // POST: Owners/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner != null) _context.Owners.Remove(owner);

        // await _context.SaveChangesAsync();
        if (!await SaveChangesAsync())
        {
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }


    private bool OwnerExists(int id)
    {
        return _context.Owners.Any(e => e.Id == id);
    }
}