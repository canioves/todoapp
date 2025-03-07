namespace API.DTOs
{
    public class TaskItemDTO
    {
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
