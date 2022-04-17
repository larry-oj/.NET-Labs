namespace RestaurantAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        this._menuService = menuService;
    }

    [HttpGet]
    public IActionResult GetMenu()
    {
        try
        {
            var response = new GetMenuResponse
            {
                Menu = _menuService.GetMenu()
            };

            return Ok(response);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("GetMeal")]
    public IActionResult GetMeal([FromQuery(Name = "Name")] string name)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = new GetMealResponse
            {
                MenuItem = _menuService.GetMeal(name)
            };

            return Ok(response);
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
    [Route("AddMeal")]
    public async Task<IActionResult> AddMeal([FromBody] AddMealRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _menuService.ComposeMealAsync(request.Name, request.Ingredients, request.NormalPrice, request.LargePrice);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }

        return Ok();
    }

    [HttpDelete]
    [Route("DeleteMeal")]
    public async Task<IActionResult> DeleteMeal([FromQuery(Name = "Name")] string name)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _menuService.DeleteMealAsync(name);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500);
        }

        return Ok();
    }
}