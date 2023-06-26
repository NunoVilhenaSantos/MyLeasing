using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Repositories.Interfaces;
using MyLeasing.Web.Data.Seeders;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using Serilog;

namespace MyLeasing.Web.Controllers;

public class LesseesController : Controller
{
    public LesseesController(
        IUserHelper userHelper,
        IImageHelper imageHelper,
        IStorageHelper storageHelper,
        IConverterHelper converterHelper,
        ILesseeRepository lesseeRepository
    )
    {
        _userHelper = userHelper;
        _imageHelper = imageHelper;
        _storageHelper = storageHelper;
        _converterHelper = converterHelper;
        _lesseeRepository = lesseeRepository;
    }


    // GET: Lessees
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(
            View(_lesseeRepository.GetAll()
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName))
        );
    }


    // GET: Lessees/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

        return View(lessee);
    }

    [Authorize(Roles = "Admin")]
    // GET: Lessees/Create
    public IActionResult Create()
    {
        return View();
    }


    // POST: Lessees/Create
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
        int id, LesseeViewModel lesseeViewModel)
    {
        if (id != lesseeViewModel.Id) return NotFound();

        // TODO: Model validation, pending to implement
        // TODO: Microsoft BUG: ModelState.IsValid is always false
        // if (!ModelState.IsValid) return View(lesseeViewModel);

        try
        {
            var filePath = lesseeViewModel.ProfilePhotoUrl;
            var fileStorageId = lesseeViewModel.ProfilePhotoId;

            if (lesseeViewModel.ImageFile is {Length: > 0})
            {
                filePath = await _imageHelper.UploadImageAsync(
                    lesseeViewModel.ImageFile, "lessees");

                fileStorageId = await _storageHelper.UploadStorageAsync(
                    lesseeViewModel.ImageFile, "lessees");
            }

            var lessee = _converterHelper.ToLessee(
                lesseeViewModel, filePath, fileStorageId, true);


            // TODO: Pending to improve
            // lessee.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            // TODO: use seedDb, pending to implement
            var user = await _userHelper
                .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
            lessee.User = user ?? lesseeViewModel.User;

            await _lesseeRepository.CreateAsync(lessee);

            if (!await _lesseeRepository.SaveAllAsync())
                Log.Logger.Error("Error creating owner: {0}, {1}",
                    lessee.Id, lessee.FullName);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _lesseeRepository.ExistAsync(lesseeViewModel.Id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Lessees/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

        if (lessee == null) return NotFound();

        var lesseeViewModel =
            _converterHelper.ToLesseeViewModel(lessee);

        return View(lesseeViewModel);
    }


    // POST: Lessees/Edit/5
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
        int id, LesseeViewModel lesseeViewModel)
    {
        if (id != lesseeViewModel.Id) return NotFound();

        // TODO: Model validation, pending to implement
        // TODO: Microsoft BUG: ModelState.IsValid is always false
        // if (!ModelState.IsValid) return View(lesseeViewModel);

        try
        {
            var filePath = lesseeViewModel.ProfilePhotoUrl;
            var fileStorageId = lesseeViewModel.ProfilePhotoId;

            if (lesseeViewModel.ImageFile is {Length: > 0})
            {
                filePath = await _imageHelper.UploadImageAsync(
                    lesseeViewModel.ImageFile, "lessees");

                fileStorageId = await _storageHelper.UploadStorageAsync(
                    lesseeViewModel.ImageFile, "lessees");
            }

            var lessee = _converterHelper.ToLessee(
                lesseeViewModel, filePath, fileStorageId, false);


            // TODO: Pending to improve
            // lessee.User = await _userHelper.GetUserByEmailAsync(User.Identity?.Name);
            var user = await _userHelper
                .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
            lessee.User = user ?? lesseeViewModel.User;

            await _lesseeRepository.UpdateAsync(lessee);

            if (!await _lesseeRepository.SaveAllAsync())
                Log.Logger.Error(
                    "Error creating owner: {0}, {1}",
                    lessee.Id, lessee.FullName);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _lesseeRepository.ExistAsync(lesseeViewModel.Id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Lessees/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

        if (lessee == null) return NotFound();

        return View(lessee);
    }


    // POST: Lessees/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lessee = await _lesseeRepository.GetByIdAsync(id);

        if (lessee == null) return RedirectToAction(nameof(Index));

        await _lesseeRepository.DeleteAsync(lessee);

        if (!await _lesseeRepository.SaveAllAsync())
            Log.Logger.Error(
                "Error creating owner: {0}, {1}",
                lessee.Id, lessee.FullName);

        return RedirectToAction(nameof(Index));
    }


    // private Task<Lessees?> LesseeExists(int id)
    // {
    //     return (_lesseeRepository.GetByIdAsync(id));
    // }

    #region Attribues

    private readonly IUserHelper _userHelper;
    private readonly IImageHelper _imageHelper;
    private readonly IStorageHelper _storageHelper;
    private readonly IConverterHelper _converterHelper;
    private readonly ILesseeRepository _lesseeRepository;

    #endregion
}