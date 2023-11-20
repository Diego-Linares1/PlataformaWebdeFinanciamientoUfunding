using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWFU.DAL;
using PWFU.Models;
using PWFU.ViewModels;

namespace PWFU.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _context.Projects
            .Include(x => x.Category)
            .Include(x => x.Student)
            .Select(x => new GetProject()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.Image,
                ProjectGoal = x.ProjectGoal,
                MoneyGoal = x.MoneyGoal,
                DeadLine = x.DeadLine,
                History = x.History,
                CategoryName = x.Category.Name,
            })
            .ToListAsync();

        var model = new GetHomeProjects()
        {
            Projects = projects
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}