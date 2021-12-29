using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public TodoController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<ActionResult> GetTodos()
    {
        try
        {
             var result = await appDbContext.Todos.ToListAsync();
             return Ok(result);
        }
        catch (Exception)
        {   
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    { 
        try
        {
            var result = await appDbContext.Todos.FirstOrDefaultAsync(e => e.Id == id);

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo(Todo todo)
    {
        try
        {
            if(todo == null) return BadRequest();

            await appDbContext.Todos.AddAsync(todo);
            await appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddTodo),
                new { id = todo.Id }, todo);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error adding a new todo");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Todo>> EditTodo(int id, Todo todo)
    {
        try
        {
            if (id != todo.Id) return BadRequest("Todo Id mismatch");
            if(todo == null) throw new ArgumentNullException(message: "Todo cannot be null", null);
            var result = await appDbContext.Todos.FirstOrDefaultAsync(x => x.Id == todo.Id);
            if (result == null)
            {
                return NotFound($"Todo with Id = {id} not found");
            }

            result.Name = todo.Name;
            result.Description = todo.Description;
            result.DueDate = todo.DueDate;
            await appDbContext.SaveChangesAsync();
            return Ok(result);

        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error updating todo");
        }

    }   
}