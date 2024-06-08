using Microsoft.AspNetCore.Mvc;
using eComWebApp.Models;
using eComWebApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var orderDetails = await _service.GetByIdAsync(id);
        if (orderDetails == null) return View("NotFound");
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
        await _service.AddAsync(order);
        return RedirectToAction(nameof(Index));
    }

    // GET: Orders/Edit/:id
    public async Task<IActionResult> Edit(int id)
    {
        var orderEdit = await _service.GetByIdAsync(id);
        if (orderEdit == null) { return View("NotFound"); }
        return View(orderEdit);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Order order)
    {
        if (id != order.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(order);
        }

        try
        {
            await _service.UpdateAsync(order);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(order);
        }
    }



    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
