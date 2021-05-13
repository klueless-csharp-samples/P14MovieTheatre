namespace MovieTheatre.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using MovieTheatre.Context;
  using MovieTheatre.Data;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.EntityFrameworkCore;

  public class SessionController : Controller
  {
    // Should be using DomainContext instead of MsDbContext
    // private readonly DomainContext _context;
    private readonly MsDbContext _context;

    public SessionController(MsDbContext context)
    {
        _context = context;
    }

    // GET: Session
    public async Task<IActionResult> Index(int id)
    {
      var sessions = await _context
        .Sessions
        .Include(s=>s.Movie)
        .Where(s=>s.MovieId == id)
        .ToListAsync();

      return View(sessions);
    }

    // GET: Session/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Sessions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    // GET: Session/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Session/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    // Id,FirstName,LastName,Phone,BirthDate
    public async Task<IActionResult> Create([Bind("Id,MovieId,Time")] Session session)
    {
      if (ModelState.IsValid)
      {
          _context.Add(session);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
      }

      return View(session);
    }

    // GET: Session/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Sessions.FindAsync(id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    // POST: Session/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,Time")] Session session)
    {
      if (id != session.Id)
      {
          return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
            _context.Update(session);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(session.Id))
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

      return View(session);
    }

    // GET: Session/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = await _context.Sessions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    // POST: Session/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var session = await _context.Sessions.FindAsync(id);
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SessionExists(int id)
    {
        return _context.Sessions.Any(e => e.Id == id);
    }
  }
}
