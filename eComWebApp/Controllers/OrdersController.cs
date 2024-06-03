using Microsoft.AspNetCore.Mvc;
using eComWebApp.Data;
using eComWebApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders.ToListAsync();
        return View(orders);
    }
}
