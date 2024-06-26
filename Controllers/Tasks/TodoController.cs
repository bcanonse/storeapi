namespace StoreApi.Controllers.Tasks;


[NestedRoute("[controller]", typeof(TodoController))]
[ApiVersion("1.0")]
public class TodoController(
    ITodoServices service
) : ControllerApiBase
{
    private readonly ITodoServices _services = service;

    [HttpPost]
    public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _services.CreateTodoAsync(request);
            return StatusCode(201, new { message = "Blog post successfully created" });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    message = "An error occurred while creating the  crating Todo Item",
                    error = ex.Message
                }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var todo = await _services.GetAllAsync();
            if (todo == null || !todo.Any())
            {
                return Ok(new { message = "No Todo Items  found" });
            }
            return Ok(new { message = "Successfully retrieved all blog posts", data = todo });

        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    message = "An error occurred while retrieving all Tood it posts",
                    error = ex.Message
                }
            );
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            var todo = await _services.GetByIdAsync(id);
            return Ok(
                new
                {
                    message = $"Successfully retrieved Todo item with Id {id}.",
                    data = todo
                }
            );
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    message = $"An error occurred while retrieving the Todo item with Id {id}.",
                    error = ex.Message
                }
            );
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {

            var todo = await _services.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new { message = $"Todo Item  with id {id} not found" });
            }

            await _services.UpdateTodoAsync(id, request);
            return Ok(new { message = $" Todo Item  with id {id} successfully updated" });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while updating blog post with id {id}", error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTodoAsync(Guid id)
    {
        try
        {
            await _services.DeleteTodoAsync(id);
            return NoContent();

        }
        catch (Exception ex)
        {
            return StatusCode(
                500, 
                new 
                { 
                    message = $"An error occurred while deleting Todo Item  with id {id}",
                    error = ex.Message 
                }
            );

        }
    }
}