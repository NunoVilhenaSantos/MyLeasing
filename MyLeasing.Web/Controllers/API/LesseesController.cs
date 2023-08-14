using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class LesseesController : ControllerBase
{
    private readonly DataContextMSSQL _contextMssql;

    public LesseesController(DataContextMSSQL contextMssql)
    {
        _contextMssql = contextMssql;
    }


    // GET: api/Lessees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lessee>>> GetLessee()
    {
        return await _contextMssql.Lessees
            .Include(t => t.User)
            .ToListAsync();
        // return Ok(_contextMssql.Owners.Include(t => t.User));
    }


    // GET: api/Lessees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Lessee>> GetLessee(int id)
    {
        var lessee = await _contextMssql.Lessees.FindAsync(id);

        if (lessee == null)
            return NotFound();

        return lessee;
    }


    // PUT: api/Lessees/5
    //
    // To protect from overposting attacks,
    // see https://go.microsoft.com/fwlink/?linkid=2123754
    //
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLessee(int id, Lessee lessee)
    {
        if (id != lessee.Id) return BadRequest();

        _contextMssql.Entry(lessee).State = EntityState.Modified;

        try
        {
            await _contextMssql.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LesseeExists(id)) return NotFound();

            throw;
        }

        return NoContent();
    }


    // POST: api/Lessees
    //
    // To protect from overposting attacks,
    // see https://go.microsoft.com/fwlink/?linkid=2123754
    //
    [HttpPost]
    public async Task<ActionResult<Lessee>> PostLessee(Lessee lessee)
    {
        _contextMssql.Lessees.Add(lessee);

        await _contextMssql.SaveChangesAsync();

        return CreatedAtAction("GetLessee",
            new { id = lessee.Id }, lessee);
    }


    // DELETE: api/Lessees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessee(int id)
    {
        var lessee = await _contextMssql.Lessees.FindAsync(id);

        if (lessee == null)
            return NotFound();

        _contextMssql.Lessees.Remove(lessee);

        await _contextMssql.SaveChangesAsync();

        return NoContent();
    }


    private bool LesseeExists(int id)
    {
        return (_contextMssql.Lessees?
                .Any(e => e.Id == id))
            .GetValueOrDefault();
    }
}