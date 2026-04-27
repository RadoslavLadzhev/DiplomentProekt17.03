using ASPBMWElit.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPBMWElit.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class InquiringsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;
        
        public InquiringsController(ApplicationDbContext context,
          UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Inquirings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inquirings.Include(i => i.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inquirings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquiring = await _context.Inquirings
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inquiring == null)
            {
                return NotFound();
            }

            return View(inquiring);
        }

        // GET: Inquirings/Create
        public IActionResult Create()
        {
            ViewData["CarId"]=new SelectList(_context.Cars, "Id", "Model");
            //ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            return View();
        }

        // POST: Inquirings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,CarId,Message,InspectionDate,CreateAt")] Inquiring inquiring)
        {
            inquiring.InspectionDate = DateTime.Now;
            inquiring.ClientId = _userManager.GetUserId(User);

          
            if (!_context.Cars.Any(c => c.Id == inquiring.CarId))
            {
                ModelState.AddModelError("CarId", "Избраният автомобил не съществува!");
                ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model", inquiring.CarId);
                return View(inquiring);
            }

            if (ModelState.IsValid)
            {
                _context.Inquirings.Add(inquiring);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Model", inquiring.CarId);
            return View(inquiring);
        }

        // GET: Inquirings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquiring = await _context.Inquirings.FindAsync(id);
            if (inquiring == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", inquiring.ClientId);
            return View(inquiring);
        }

        // POST: Inquirings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ClientId,CarId,Message,InspectionDate,CreateAt")] Inquiring inquiring)
        {
            if (id != inquiring.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inquiring);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InquiringExists(inquiring.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", inquiring.ClientId);
            return View(inquiring);
        }

        // GET: Inquirings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquiring = await _context.Inquirings
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inquiring == null)
            {
                return NotFound();
            }

            return View(inquiring);
        }

        // POST: Inquirings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var inquiring = await _context.Inquirings.FindAsync(id);
            if (inquiring != null)
            {
                _context.Inquirings.Remove(inquiring);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InquiringExists(int? id)
        {
            return _context.Inquirings.Any(e => e.Id == id);
        }
    }
}
