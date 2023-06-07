﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class OwnersController : ControllerBase
{
    private readonly DataContext _context;

    public OwnersController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Owners
    [HttpGet]
    public IActionResult GetOwners()
    {
        return Ok(_context.Owners.Include(t => t.User));
    }


    // GET: api/Owners/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Owner>> GetOwner(int id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner == null)
            return NotFound();

        return owner;
    }

    // PUT: api/Owners/5
    // To protect from overposting attacks,
    // see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOwner(int id, Owner owner)
    {
        if (id != owner.Id)
            return BadRequest();

        _context.Entry(owner).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (OwnerExists(id)) throw;

            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Owners
    // To protect from overposting attacks,
    // see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Owner>> PostOwner(Owner owner)
    {
        _context.Owners.Add(owner);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            "GetOwner", new {id = owner.Id}, owner);
    }

    // DELETE: api/Owners/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOwner(int id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner == null)
            return NotFound();

        _context.Owners.Remove(owner);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OwnerExists(int id)
    {
        return (_context.Owners?.Any(e => e.Id == id))
            .GetValueOrDefault();
    }
}