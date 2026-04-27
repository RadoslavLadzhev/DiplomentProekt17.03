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
  
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cars.Include(c => c.Equipments).Include(c => c.FuelTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Equipments)
                .Include(c => c.FuelTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name");
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "Name");
            ViewData["CarType"] = new SelectList(Enum.GetValues(typeof(TypeAuto)));
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create([Bind("Name,CatalogNumber,Model,EquipmentId,Descpription,FuelTypeId,HorsePower,Acceleration,ImageUrl,Price,CreatedAt")] Car car)
        {

            if (!await _context.Equipments.AnyAsync(e => e.Id == car.EquipmentId))
                ModelState.AddModelError("EquipmentId", "Невалиден EquipmentId");

            if (!await _context.FuelTypes.AnyAsync(f => f.Id == car.FuelTypeId))
                ModelState.AddModelError("FuelTypeId", "Невалиден FuelTypeId");

            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync(); // Тук вече няма да пада
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", car.EquipmentId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "Name", car.FuelTypeId);
            ViewData["CarType"] = new SelectList(Enum.GetValues(typeof(TypeAuto)), car.CarType);
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", car.EquipmentId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "Name", car.FuelTypeId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatalogNumber,Model,EquipmentId,Description,FuelTypeId,HorsePower,Acceleration,ImageUrl,Price,CreatedAt")] Car car)
        {

            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", car.EquipmentId);
            ViewData["FuelTypeId"] = new SelectList(_context.FuelTypes, "Id", "Name", car.FuelTypeId);
            return View(car);
        }
        
        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Equipments)
                .Include(c => c.FuelTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookViewing(int carId)
        {
            var inquiry = new Inquiring
            {
                CarId = carId,

                CreateAt = DateTime.Now
            };

            _context.Inquirings.Add(inquiry);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", "Home");
        }
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
        
    }
}
