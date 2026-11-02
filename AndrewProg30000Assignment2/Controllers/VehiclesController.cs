using AndrewProg30000Assignment2.Data;
using AndrewProg30000Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndrewProg30000Assignment2.Controllers;

public class VehiclesController : Controller
{
    private readonly AppDbContext _context;

    public VehiclesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var vehicles = await _context.Vehicles.ToListAsync();
        return View(vehicles);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);
        if (vehicle == null) return NotFound();
        return View(vehicle);
    }

    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Filter(VehicleType? type)
    {
        ViewBag.SelectedType = type;
        ViewBag.Searched = type != null;
        if (type == null) return View(new List<Vehicle>());

        var vehicles = await _context.Vehicles
            .Where(v => v.Type == type)
            .OrderBy(v => v.Make)
            .ToListAsync();
        return View(vehicles);
    }

    public async Task<IActionResult> PriceSearch(decimal? price)
    {
        ViewBag.Price = price;
        ViewBag.Searched = price != null;
        if (price == null) return View(new List<Vehicle>());

        var vehicles = await _context.Vehicles
            .Where(v => v.Price > price)
            .OrderByDescending(v => v.Price)
            .ToListAsync();
        return View(vehicles);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Make,Type,PurchaseDate,Year,Price")] Vehicle vehicle)
    {
        if (!ModelState.IsValid) return View(vehicle);
        _context.Add(vehicle);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null) return NotFound();
        return View(vehicle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("VehicleId,Make,Type,PurchaseDate,Year,Price")] Vehicle vehicle)
    {
        if (id != vehicle.VehicleId) return NotFound();
        if (!ModelState.IsValid) return View(vehicle);
        _context.Update(vehicle);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);
        if (vehicle == null) return NotFound();
        return View(vehicle);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}