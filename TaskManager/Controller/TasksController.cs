namespace TaskManager.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Data;

[ApiController]
[Route("api/[controller]")]
public class TasksController: ControllerBase
{
    private readonly AppDbContext _context;
    public TasksController(AppDbContext context) => _context = context;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> Get() => 
    await _context.Tasks.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Post(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new {id = task.Id}, task);
    }
    
    
}