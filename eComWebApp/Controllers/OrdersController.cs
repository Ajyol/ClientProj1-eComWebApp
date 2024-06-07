using Microsoft.AspNetCore.Mvc;
using eComWebApp.Data;
using eComWebApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eComWebApp.Data.Services;

public class OrdersController : Controller
{
    private readonly IOrdersService _service;

    public OrdersController(IOrdersService service)
    {
        _service = service;
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
        var orders = await _service.GetAll();
        return View(orders);
    }

    // GET: Orders/Details/:id

    public async Task<IActionResult> Details(int id)
    {
        var orderDetails = _service.GetByIdAsync(id);
        if (orderDetails == null) return View("Error");
        return View(orderDetails);
    }

    // GET: Orders/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }
        _service.AddAsync(order);
        return RedirectToAction(nameof(Index));
    }

    // GET: Orders/Edit/:id
    public IActionResult Edit(int id)
    {
        var orderEdit = _service.GetByIdAsync(id);
        if (orderEdit != null) { return  View("Empty"); }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }
        _service.AddAsync(order);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
