using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        readonly TodoAppContext _context;


        public TodoController(TodoAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод возвращает список todos
        /// </summary>
        [HttpGet]
        [Route("GetTodos")]
        public IActionResult GetTodo()
        {
            var todos = _context.Todo.ToList();
            return Ok(todos);
        }

        
        [HttpPost]
        [Route("CreateTodo")]
        public JsonResult CreateTodo([FromBody] Todo todo)
        {

            _context.Add(todo);
            _context.SaveChanges();

            return new JsonResult(todo);
        }

        [HttpGet("{id}")]
        [Route("GetTodoItemById")]
        public async Task<ActionResult<Todo>> GetTodoItem(Guid id)
        {
            var todoItem = await _context.Todo.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpDelete("{id}")]
        [Route("DeleteTodoItemById")]
        public async Task<IActionResult> DeleteTodoItemById(Guid id)
        {
            var todoItem = await _context.Todo.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        

    }
}
