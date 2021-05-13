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

  public class TicketController : Controller
  {
    // Should be using DomainContext instead of MsDbContext
    // private readonly DomainContext _context;
    private readonly MsDbContext _context;

    public TicketController(MsDbContext context)
    {
        _context = context;
    }

    // GET: Ticket
    public async Task<IActionResult> Index()
    {
        return View(await _context.Tickets.ToListAsync());
    }

    // GET: Ticket/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // GET: Ticket/Create
    public IActionResult Create(int session_id)
    {
      var session = await _context
        .Sessions
        .Include(s=>s.Movie)
        .Where(s=>s.SessionId == session_id)
        .FirstOrDefaultAsync();

        return View();
    }

    // POST: Ticket/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    // Id,FirstName,LastName,Phone,BirthDate
    public async Task<IActionResult> Create([Bind("Id,SessionId,Name")] Ticket ticket)
    {
      if (ModelState.IsValid)
      {
          _context.Add(ticket);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
      }

      return View(ticket);
    }

    // GET: Ticket/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // POST: Ticket/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SessionId,Name")] Ticket ticket)
    {
      if (id != ticket.Id)
      {
          return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
            _context.Update(ticket);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TicketExists(ticket.Id))
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

      return View(ticket);
    }

    // GET: Ticket/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    // POST: Ticket/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TicketExists(int id)
    {
        return _context.Tickets.Any(e => e.Id == id);
    }
  }
}
