namespace StoreApi.Services.Tasks;

public sealed class TodoServices(
    TodoDbContext context,
    ILogger<TodoServices> logger,
    IMapper mapper
) : ITodoServices
{
    private readonly TodoDbContext _context = context;
    private readonly ILogger<TodoServices> _logger = logger;
    private readonly IMapper _mapper = mapper;
    public async Task CreateTodoAsync(CreateTodoRequest request)
    {
        try
        {
            var todo = _mapper.Map<Todo>(request);
            todo.CreatedAt = DateTime.UtcNow;
            _context.Todos.Add(todo);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error ocurred while creating the todo item.");
            throw new Exception("An error ocurred while creating the todo item.");
        }
    }

    public async Task DeleteTodoAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id) 
            ?? throw new NotFoundException($"No  item found with the id {id}");
        
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Todo>> GetAllAsync() =>
        await _context.Todos.ToListAsync() ??
            throw new NotFoundException("No Todo items found");

    public async Task<Todo> GetByIdAsync(Guid id) =>
        await _context.Todos.FindAsync(id) ??
            throw new NotFoundException($"No Todo item with Id {id} found.");

    public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
    {
        try
        {
            var todo =
                await _context.Todos.FindAsync(id) ??
                throw new NotFoundException($"Todo item with id {id} not found.");


            if (!string.IsNullOrEmpty(request.Title))
            {
                todo.Title = request.Title;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                todo.Description = request.Description;
            }

            if (request.IsComplete is not null)
            {
                todo.IsComplete = request.IsComplete.Value;
            }

            if (request.DueDate is not null)
            {
                todo.DueDate = request.DueDate.Value;
            }

            if (request.Priority is not null)
            {
                todo.Priority = request.Priority.Value;
            }

            todo.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
            throw;
        }
    }
}