using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.DataContexts;
using MyLeasing.Common.Entities;
using MyLeasing.Common.Repositories;
using Serilog;

namespace MyLeasing.Web.Controllers;

public class OwnersController : Controller
{
    private readonly IRepository _repository;

    public OwnersController(IRepository repository)
    {
        // _context = context;
        _repository = repository;
    }

    // GET: Owners
    public async Task<IActionResult> Index()
    {
        return View(_repository.GetOwners());
    }

    // GET: Owners/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var owner = _repository.GetOwner(id.Value);

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
    public async Task<IActionResult> Create(Owner owner)
    {
        if (!ModelState.IsValid) return View(owner);

        _repository.AddOwner(owner);


        if (!await _repository.SaveOwnersAsync())
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

        var owner = _repository.GetOwner(id.Value);

        return View(owner);
    }

    // POST: Owners/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Owner owner)
    {
        if (id != owner.Id) return NotFound();

        if (!ModelState.IsValid) return View(owner);

        try
        {
            _repository.UpdateOwner(owner);

            if (!await _repository.SaveOwnersAsync())
            {
                Log.Logger.Error(
                    "Error creating owner: {0}, {1}",
                    owner.Id, owner.FullName);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_repository.OwnerExists(owner.Id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Owners/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var owner = _repository.GetOwner(id.Value);

        return View(owner);
    }

    // POST: Owners/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var owner = _repository.GetOwner(id);

        _repository.RemoveOwner(owner);

        // ;
        if (!await _repository.SaveOwnersAsync())
        {
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);
        }

        return RedirectToAction(nameof(Index));
    }
}