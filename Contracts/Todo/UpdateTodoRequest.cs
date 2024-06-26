namespace StoreApi.Contracts.Todo;

public sealed class UpdateTodoRequest
{
    [StringLength(100)]
    public required string Title { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    public bool? IsComplete { get; set; } = false;

    public DateTime? DueDate { get; set; }

    [Range(1, 5)]
    public int? Priority { get; set; }
}