using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Repositories.Interfaces;
using MyLeasing.Web.Data.Seeders;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using Serilog;

namespace MyLeasing.Web.Controllers;

public class OwnersController : Controller
{
    public OwnersController(
        IUserHelper userHelper,
        IImageHelper imageHelper,
        IStorageHelper storageHelper,
        IConverterHelper converterHelper,
        IOwnerRepository ownerRepository
    )
    {
        _userHelper = userHelper;
        _imageHelper = imageHelper;
        _converterHelper = converterHelper;
        _ownerRepository = ownerRepository;
        _storageHelper = storageHelper;
    }


    // GET: Owners
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(
            View(_ownerRepository.GetAll()
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName))
        );
    }


    // GET: Owners/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

        if (owner == null) return NotFound();

        return View(owner);
    }


    // GET: Owners/Create
    public IActionResult Create()
    {
        return View();
    }


    // POST: Owners/Create
    //
    // To protect from overposting attacks,
    // enable the specific properties you want to bind to.
    //
    // For more details,
    // see http://go.microsoft.com/fwlink/?LinkId=317598.
    //
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int id, OwnerViewModel ownerViewModel)
    {
        if (id != ownerViewModel.Id) return NotFound();

        // TODO: Model validation, pending to implement
        // TODO: Microsoft BUG: ModelState.IsValid is always false
        // if (!ModelState.IsValid) return View(ownerViewModel);

        try
        {
            var filePath = ownerViewModel.ProfilePhotoUrl;
            var fileStorageId = ownerViewModel.ProfilePhotoId;

            if (ownerViewModel.ImageFile is {Length: > 0})
            {
                filePath = await _imageHelper.UploadImageAsync(
                    ownerViewModel.ImageFile, "owners");

                fileStorageId = await _storageHelper.UploadStorageAsync(
                    ownerViewModel.ImageFile, "owners");
            }

            var owner = _converterHelper.ToOwner(
                ownerViewModel, filePath, fileStorageId, true);


            // TODO: Pending to change to the logged user
            // owner.User = await _userHelper.GetUserByEmailAsync(owner.User.Email);
            // TODO: use seedDb, pending to implement
            var user = await _userHelper
                .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
            owner.User = user ?? ownerViewModel.User;

            await _ownerRepository.CreateAsync(owner);

            if (!await _ownerRepository.SaveAllAsync())
                Log.Logger.Error("Error creating owner: {0}, {1}",
                    owner.Id, owner.FullName);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _ownerRepository.ExistAsync(ownerViewModel.Id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Owners/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

        if (owner == null) return NotFound();

        var ownerViewModel =
            _converterHelper.ToOwnerViewModel(owner);

        return View(ownerViewModel);
    }


    // POST: Owners/Edit/5
    //
    // To protect from overposting attacks,
    // enable the specific properties you want to bind to.
    //
    // For more details,
    // see http://go.microsoft.com/fwlink/?LinkId=317598.
    //
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, OwnerViewModel ownerViewModel)
    {
        if (id != ownerViewModel.Id) return NotFound();

        // TODO: Model validation, pending to implement
        // TODO: Microsoft BUG: ModelState.IsValid is always false
        // if (!ModelState.IsValid) return View(ownerViewModel);

        try
        {
            var filePath = ownerViewModel.ProfilePhotoUrl;
            var fileStorageId = ownerViewModel.ProfilePhotoId;

            if (ownerViewModel.ImageFile is {Length: > 0})
            {
                filePath = await _imageHelper.UploadImageAsync(
                    ownerViewModel.ImageFile, "owners");

                fileStorageId = await _storageHelper.UploadStorageAsync(
                    ownerViewModel.ImageFile, "owners");
            }

            var owner = _converterHelper.ToOwner(
                ownerViewModel, filePath, fileStorageId, false);


            // TODO: Pending to change to the logged user
            // owner.User = await _userHelper.GetUserByEmailAsync(owner.User.Email);
            var user = await _userHelper
                .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
            owner.User = user ?? ownerViewModel.User;

            await _ownerRepository.UpdateAsync(owner);

            if (!await _ownerRepository.SaveAllAsync())
                Log.Logger.Error(
                    "Error creating owner: {0}, {1}",
                    owner.Id, owner.FullName);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _ownerRepository.ExistAsync(ownerViewModel.Id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Owners/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var owner = await _ownerRepository.GetByIdAsync(id.Value);

        if (owner == null) return NotFound();

        return View(owner);
    }


    // POST: Owners/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);

        if (owner == null) return RedirectToAction(nameof(Index));

        await _ownerRepository.DeleteAsync(owner);

        if (!await _ownerRepository.SaveAllAsync())
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                owner.Id, owner.FullName);

        return RedirectToAction(nameof(Index));
    }

    #region Attributes

    private readonly IUserHelper _userHelper;
    private readonly IImageHelper _imageHelper;
    private readonly IStorageHelper _storageHelper;
    private readonly IConverterHelper _converterHelper;
    private readonly IOwnerRepository _ownerRepository;

    #endregion

    // private Task<Owner?> OwnerExists(int id)
    // {
    //     return (_ownerRepository.GetByIdAsync(id));
    // }
}