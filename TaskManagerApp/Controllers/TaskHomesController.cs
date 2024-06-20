
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    public class TaskHomesController : Controller
    {
        private readonly TaskManagerAppContext _context;
        private readonly UserManager<UserDetail> userMg;
        private readonly string? userId_;
        public TaskHomesController(TaskManagerAppContext context, UserManager<UserDetail> _User)
        {
            _context = context;
            userMg = _User;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            ViewBag.UserName = userMg.GetUserName(HttpContext.User);

            return View(await _context.TaskHome.Where(m => m.UserId == userMg.GetUserId(HttpContext.User)).ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }

            var taskHome = await _context.TaskHome
                .FirstOrDefaultAsync(m => m.TaskId == id && m.UserId == userMg.GetUserId(HttpContext.User));
            if (taskHome == null)
            {
                return NotFound();
            }

            return View(taskHome);
        }
        public IActionResult Create()
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,TaskName,TaskDescription,Startdate,Deadline,TaskStatus")] TaskHome taskHome)
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(taskHome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskHome);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var userid = userMg.GetUserId(HttpContext.User);
            ViewBag.UserId = userid;
            if (id == null)
            {
                return NotFound();
            }

            var taskHome = await _context.TaskHome.FirstOrDefaultAsync(n => n.TaskId == id && n.UserId == userid);
            if (taskHome == null)
            {
                return NotFound();
            }
            return View(taskHome);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int TaskId, [Bind("TaskId,UserId,TaskName,TaskDescription,Startdate,Deadline,TaskStatusId")] TaskHome taskHome)
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            if (TaskId != taskHome.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskHome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskHomeExists(taskHome.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskHome);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userid = userMg.GetUserId(HttpContext.User);
            ViewBag.UserId = userid;
            if (id == null)
            {
                return NotFound();
            }

            var taskHome = await _context.TaskHome
                .FirstOrDefaultAsync(m => m.TaskId == id && m.UserId == userid);
            if (taskHome == null)
            {
                return NotFound();
            }

            return View(taskHome);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int TaskId)
        {
            var userid = userMg.GetUserId(HttpContext.User);
            ViewBag.UserId = userid;
            var taskHome = await _context.TaskHome.FirstOrDefaultAsync(m => m.TaskId == TaskId && m.UserId == userid);
            if (taskHome != null)
            {
                _context.TaskHome.Remove(taskHome);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TaskHomeExists(int id)
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            return _context.TaskHome.Any(e => e.TaskId == id && e.UserId == userMg.GetUserId(HttpContext.User));
        }
        public void exceptionactn(Exception ex)
        {
            ViewBag.UserId = userMg.GetUserId(HttpContext.User);
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

        }
    }
}
