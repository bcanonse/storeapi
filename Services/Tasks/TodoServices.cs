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

    public Task DeleteTodoAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        var todos = await _context.Todos.ToListAsync();

        if (todos is null)
        {
            throw new NotFoundException("No Todo items found");
        }

        return todos;
    }

    public Task<Todo> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
    {
        throw new NotImplementedException();
    }
}