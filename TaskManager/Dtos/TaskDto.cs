namespace TaskManager.Dtos;

public class TaskDto
{
    public int    Id          { get; set; }
    public string Title       { get; set; } = "";
    public bool   IsCompleted { get; set; }
}