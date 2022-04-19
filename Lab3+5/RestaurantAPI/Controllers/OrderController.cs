namespace RestaurantAPI.Controllers;

[ApiController]
[Route("Orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        this._orderService = orderService;
    }

    [HttpGet]
    public IActionResult GetOrders([FromQuery(Name = "Active")] bool? active)
    {
        try
        {
            var orders = _orderService.GetAll(active);
            return Ok(orders);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("Tables/{tableNum}")]
    public IActionResult GetOrder(int tableNum)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var order = _orderService.Get(tableNum);
            return Ok(order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("AddOrder")]
    public async Task<IActionResult> AddOrder([FromBody] AddOrderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var dict = new Dictionary<string, bool>();
            request.Meals.ForEach(pair => {
                dict.Add(pair.Name, pair.IsLarge);
            });

            await _orderService.ComposeOrderAsync(dict, request.TableNumber);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("Tables/{tableNum}/Complete")]
    public async Task<IActionResult> CompleteOrder(int tableNum)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _orderService.SetOrderAsCompleted(tableNum);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("Tables/{tableNum}/Delete")]
    public async Task<IActionResult> DeleteOrder(int tableNum)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _orderService.DeleteLastAsync(tableNum);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }
    }
}