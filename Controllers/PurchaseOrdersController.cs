using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BodijaApi.Data;
using BodijaApi.Models;

namespace BodijaApi.Controllers;

[ApiController]
[Route("api")]
public class PurchaseOrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseOrdersController(ApplicationDbContext context)
    {
        this._context = context;
    }

    // private IQueryable<PurchaseOrder> GetPurchaseOrdersQuery()
    // {
    //     // Why use .Select() instead of .Include()?
    //     // - Performance: You only retrieve the columns you actually use. If your Items table has 20 columns but your UI only needs 3, .Select() saves significant database I/O.
    //     // - No Circular References
    //     // - Automatic Joins
        
    //     return _context.Orders
    //     .Select(o => new PurchaseOrder 
    //     {
    //         // Map the properties of the main Order
    //         Id = o.Id,
    //         PurchaseOrderNumber = o.PurchaseOrderNumber,
    //         PhoneNo = o.PhoneNo,
    //         OrderDate = o.OrderDate,
    //         Email = o.Email,
    //         DeliveryNotes = o.DeliveryNotes,

    //         // Map the related collections and objects
    //         Items = o.Items.Select(i => new Item 
    //         {
    //             Id = i.Id,
    //             PartNumber = i.PartNumber,
    //             ProductName = i.ProductName,
    //             Quantity = i.Quantity,
    //             USPrice = i.USPrice,
    //         }).ToList(),

    //         ShippingAddress = o.ShippingAddress == null ? null : new Address 
    //         {
    //             Id = o.ShippingAddress.Id,
    //             Type = o.ShippingAddress.Type,
    //             Name = o.ShippingAddress.Name,
    //             Street = o.ShippingAddress.Street,
    //             City = o.ShippingAddress.City,
    //             State = o.ShippingAddress.State,
    //             Zip = o.ShippingAddress.Zip,
    //             Country = o.ShippingAddress.Country
    //         },

    //         BillingAddress = o.BillingAddress == null ? null : new Address 
    //         {
    //             Id = o.BillingAddress.Id,
    //             Type = o.BillingAddress.Type,
    //             Name = o.BillingAddress.Name,
    //             Street = o.BillingAddress.Street,
    //             City = o.BillingAddress.City,
    //             State = o.BillingAddress.State,
    //             Zip = o.BillingAddress.Zip,
    //             Country = o.BillingAddress.Country
    //         }
    //     });
    // }
    
    private IQueryable<PurchaseOrder> GetPurchaseOrdersQuery()
    {
        return _context.Orders
            .Include(o => o.Items)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress);
    }

    // POST: api/orders
    [HttpPost("orders")]
    public async Task <ActionResult<PurchaseOrder>> CreateOrder(PurchaseOrder order)
    {
        if (order == null) return BadRequest("Order data is required.");

        _context.Orders.Add(order);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            return NotFound($"Failed creating New order!");
        }

        return Ok(order.PurchaseOrderNumber);
    }

    // GET: api/orders
    [HttpGet("orders")]
    public async Task <ActionResult<IEnumerable<PurchaseOrder>>> Get()
    {
        var orders = await GetPurchaseOrdersQuery().ToListAsync();

        if (orders == null || !orders.Any()) 
            return NoContent();

        return Ok(orders);
    }

    // GET: api/order/{orderNumber}
    [HttpGet("order/{orderNumber}")]
    public async Task <ActionResult<PurchaseOrder>> GetOrderByNumber(String orderNumber)
    {
        var order = await GetPurchaseOrdersQuery()
            .FirstOrDefaultAsync(o => o.PurchaseOrderNumber == orderNumber);

        if (order == null) 
            return NotFound($"Order: {orderNumber} not found.");

        return Ok(order);
    }

    // PUT: api/order/{id}
    [HttpPut("order/{id}")]
    public async Task <ActionResult> UpdateOrder(int id, PurchaseOrder order)
    {
        if (order == null)
            return BadRequest("Order data is required."); 

        if (id != order.Id)
            return BadRequest("Order ID mismatch!."); 
            
        _context.Update(order);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            if(!_context.Orders.Any(e => e.Id == id))
            {
                return NotFound($"Order: {id} can not be updated.");
            }
        }

        return Ok(order.PurchaseOrderNumber);
    }

}
