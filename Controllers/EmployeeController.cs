using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class EmployeeController : Controller
{
	private readonly AppDbContext _context;

	public EmployeeController(AppDbContext context)
	{
		_context = context;
	}

	// Index - List Employees
	public async Task<IActionResult> Index()
	{
		var employees = await _context.Employees.ToListAsync();
		return View(employees);
	}

	// Create - GET
	public IActionResult Create()
	{
		return View();
	}

	// Create - POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Employee employee)
	{
		if (ModelState.IsValid)
		{
			_context.Add(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View(employee);
	}

	// Edit - GET
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null) return NotFound();

		var employee = await _context.Employees.FindAsync(id);
		if (employee == null) return NotFound();

		return View(employee);
	}

	// Edit - POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, Employee employee)
	{
		if (id != employee.Id) return NotFound();

		if (ModelState.IsValid)
		{
			_context.Update(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View(employee);
	}

	// Delete - GET
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null) return NotFound();

		var employee = await _context.Employees.FindAsync(id);
		if (employee == null) return NotFound();

		return View(employee);
	}

	// Delete - POST
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var employee = await _context.Employees.FindAsync(id);
		if (employee != null)
		{
			_context.Employees.Remove(employee);
			await _context.SaveChangesAsync();
		}
		return RedirectToAction(nameof(Index));
	}
}
