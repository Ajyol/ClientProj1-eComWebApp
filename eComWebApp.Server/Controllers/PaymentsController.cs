using eComWebApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PaymentsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("create-checkout-session")]
    public async Task<ActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = request.Services.Select(service => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = service.Name,
                    },
                    UnitAmount = service.Price * 100,
                },
                Quantity = 1,
            }).ToList(),
            Mode = "payment",
            SuccessUrl = $"{Request.Scheme}://{Request.Host}/success?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{Request.Scheme}://{Request.Host}/cancel",
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return Ok(new { sessionId = session.Id });
    }
}
