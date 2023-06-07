using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Seeders;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers;

public class LesseesController : Controller
{
    public LesseesController(
        IUserHelper userHelper,
        IImageHelper imageHelper,
        IConverterHelper converterHelper,
        // ILesseeRepository lesseeRepository,
        DataContext context
    )
    {
        _userHelper = userHelper;
        _imageHelper = imageHelper;
        _converterHelper = converterHelper;
        // _lesseeRepository = lesseeRepository;
        _context = context;
    }


    // GET: Lessees
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(
            View(_context.Lessee
                .OrderBy(l => l.FirstName)
                .ThenBy(l => l.LastName)));

        // return View(_lesseeRepository.GetAll().OrderBy(p => p.Name));

        // return Task.FromResult<IActionResult>(
        //     View(_ownerRepository.GetAll()
        //         .OrderBy(o => o.FirstName)
        //         .ThenBy(o => o.LastName))
        // );
    }


    // GET: Lessees/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _context.Lessee
            .FirstOrDefaultAsync(m => m.Id == id);

        if (lessee == null) return NotFound();

        return View(lessee);
    }


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
    public async Task<IActionResult> Create(LesseeViewModel lesseeViewModel)
    {
        // TODO: Model validation, pending to implement
        // TODO: Microsoft BUG: ModelState.IsValid is always false
        // if (!ModelState.IsValid) return View(lesseeViewModel);

        var filePath = lesseeViewModel.ProfilePhotoUrl;

        // if (lesseeViewModel.ImageFile is {Length: > 0})
        //     filePath = await _imageHelper.UploadImageAsync(
        //         lesseeViewModel.ImageFile, GetType().Name);
        if (lesseeViewModel.ImageFile is {Length: > 0})
            filePath = await _imageHelper.UploadImageAsync(
                lesseeViewModel.ImageFile, "lessees");

        var lessee = _converterHelper.ToLessee(
            lesseeViewModel, filePath, true);


        // TODO: Pending to improve
        // lessee.User = await _userHelper.GetUserByEmailAsync(
        //     this.User.Identity.Name);
        // TODO: use seedDb, pending to implement
        var user = await _userHelper
            .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
        lessee.User = user ?? lesseeViewModel.User;


        await _context.Lessee.AddAsync(lessee);
        // await _lesseeRepository.CreateAsync(lessee);

        // if (!await _ownerRepository.SaveAllAsync())
        //     Log.Logger.Error(
        //         "Error creating owner: {0}, {1}",
        //         owner.Id, owner.FullName);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    // GET: Lessees/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _context.Lessee.FindAsync(id);

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

            if (lesseeViewModel.ImageFile is {Length: > 0})
                filePath = await _imageHelper.UploadImageAsync(
                    lesseeViewModel.ImageFile, "lessees");

            var lessee = _converterHelper.ToLessee(
                lesseeViewModel, filePath, false);

            // TODO: Pending to improve
            // lessee.User = await _userHelper.GetUserByEmailAsync(
            //     User.Identity?.Name);
            var user = await _userHelper
                .GetUserByEmailAsync(SeedDb.MyLeasingAdminsNuno);
            lessee.User = user ?? lesseeViewModel.User;

            _context.Lessee.Update(lessee);
            // await _lesseeRepository.UpdateAsync(lessee);

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LesseeExists(lesseeViewModel.Id)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: Lessees/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var lessee = await _context.Lessee
            .FirstOrDefaultAsync(m => m.Id == id);

        if (lessee == null) return NotFound();

        return View(lessee);
    }


    // POST: Lessees/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lessee = await _context.Lessee.FindAsync(id);

        if (lessee != null) _context.Lessee.Remove(lessee);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    private bool LesseeExists(int id)
    {
        return (_context.Lessee?
                .Any(e => e.Id == id))
            .GetValueOrDefault();
    }

    #region Attribues

    private readonly DataContext _context;
    private readonly IUserHelper _userHelper;
    private readonly IImageHelper _imageHelper;

    private readonly IConverterHelper _converterHelper;
    // private readonly ILesseeRepository _lesseeRepository;

    #endregion
}