using Microsoft.AspNetCore.Mvc;
using eComWebApp.Models;
using eComWebApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _service;

    public OrdersController(IOrdersService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var orderDetails = await _service.GetByIdAsync(id);
        if (orderDetails == null) return NotFound();
        return Ok(orderDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _service.AddAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _service.UpdateAsync(id, order);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
