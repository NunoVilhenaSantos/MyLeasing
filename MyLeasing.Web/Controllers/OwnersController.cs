using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Entities;
using MyLeasing.Common.Repositories;
using MyLeasing.Common.Repositories.OLD;
using Serilog;

namespace MyLeasing.Web.Controllers;

public class OwnersController : Controller
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnersController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
        // _context = context;
        // _repository = repository;
    }


    // GET: Owners
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(
            View(_ownerRepository.GetAll())
        );
    }


    // GET: Owners/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

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

        await _ownerRepository.CreateAsync(owner);

        if (!await _ownerRepository.SaveAllAsync())
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);

        return RedirectToAction(nameof(Index));
    }


    // GET: Owners/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

        return View(owner);
    }


    // POST: Owners/Edit/5
    //
    // To protect from overposting attacks,
    // enable the specific properties you want to bind to.
    //
    // For more details,
    // see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Owner owner)
    {
        if (id != owner.Id) return NotFound();

        if (!ModelState.IsValid) return View(owner);

        try
        {
            await _ownerRepository.UpdateAsync(owner);

            if (!await _ownerRepository.SaveAllAsync())
                Log.Logger.Error(
                    "Error creating owner: {0}, {1}",
                    owner.Id, owner.FullName);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _ownerRepository.ExistAsync(owner.Id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Owners/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

        return View(owner);
    }


    // POST: Owners/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);

        await _ownerRepository.DeleteAsync(owner);

        if (!await _ownerRepository.SaveAllAsync())
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);

        return RedirectToAction(nameof(Index));
    }
}