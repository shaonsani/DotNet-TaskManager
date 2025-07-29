using AutoMapper;
using TaskManager.Dtos;

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
    private readonly IMapper _mapper;
    private readonly ILogger<TasksController> _logger;
    public TasksController(
        AppDbContext context,
        IMapper mapper,
        ILogger<TasksController> logger)
    {
        _context = context;
        _mapper  = mapper;
        _logger  = logger;
    }


    [HttpGet]
    public async Task<IEnumerable<TaskDto>> Get()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null)
            return NotFound(new { Message = $"Task with ID {id} not found." });
        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Post(TaskDto dto)
    {
        _logger.LogInformation("Creating task with title '{Title}'", dto.Title);
        var entity = _mapper.Map<TaskItem>(dto);
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<TaskDto>(entity);
        return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TaskDto updatedDto)
    {
        if (id != updatedDto.Id)
            return BadRequest(new { Message = "ID in URL and body must match." });
        
        var existing = await _context.Tasks.FindAsync(id);
        if (existing is null)
            return NotFound(new { Message = $"Task with ID {id} not found." });;
        
        _mapper.Map(updatedDto, existing);

        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _context.Tasks.FindAsync(id);
        if (existing is null)
            return NotFound(new { Message = $"Task with ID {id} not found." });

        _context.Tasks.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }



}