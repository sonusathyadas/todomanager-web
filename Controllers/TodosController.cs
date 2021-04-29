using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoManager.Models;
using System.Linq;
using System.Threading.Tasks;


namespace TodoManager.Controllers
{
    public class TodosController : Controller
    {
        private readonly ILogger<TodosController> logger;
        private readonly TodoDataContext db;

        public TodosController(ILogger<TodosController> logger, TodoDataContext dbContext)
        {
            this.logger = logger;
            db = dbContext;
        }

        public IActionResult Index()
        {
            var data = db.Todos.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(todo);
            }
            await db.Todos.AddAsync(todo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, [FromBody]Todo todo)
        {
            var temp = await db.Todos.FindAsync(id);
            if(temp!=null){
                temp.IsCompleted = todo.IsCompleted;
                await db.SaveChangesAsync();
                return Json(temp);
            }else{
                return Json(new { Error="Todo item not found"});
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var temp = await db.Todos.FindAsync(id);
            if(temp!=null){
                db.Todos.Remove(temp);                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }else{
                return RedirectToAction("Index");
            }
        }
    }
}