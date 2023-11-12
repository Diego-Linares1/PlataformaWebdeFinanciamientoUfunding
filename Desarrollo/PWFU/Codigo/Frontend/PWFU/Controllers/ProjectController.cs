using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWFU.DAL;
using PWFU.Models;
using PWFU.ViewModels;

namespace PWFU.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await _context.Projects
            .Include(x => x.Category)
            .Include(x => x.Student)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (project == null)
            return NotFound();

        var donations = await _context.Donations
            .Where(x => x.ProjectId == id)
            .ToListAsync();

        var model = new GetProject()
        {
            Id = project.Id,
            Name = project.Name,
            ImagePath = project.Image,
            ProjectGoal = project.ProjectGoal,
            MoneyGoal = project.MoneyGoal,
            DeadLine = project.DeadLine,
            History = project.History,
            CategoryName = project.Category.Name,
            Donations = donations
        };
        return View(model);
    }

    public async Task<IActionResult> Search(string keyWord)
    {
        var projects = await _context.Projects
            .Include(x => x.Category)
            .Include(x => x.Student)
            .Where(x => x.Name.ToUpper().Contains(keyWord.ToUpper()) || x.Category.Name.ToUpper().Contains(keyWord.ToUpper()))
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

        var model = new SearchProjects()
        {
            Projects = projects
        };

        return View(model);
    }


    public async Task<IActionResult> CreateProject()
    {
        var categories = await _context.Categories.ToListAsync();
        var model = new CreateProject
        {
            Categories = categories
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProject model)
    {
        if (ModelState.IsValid)
        {
            var student = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            var project = new Project
            {
                Name = model.Name,
                ProjectGoal = model.ProjectGoal,
                MoneyGoal = model.MoneyGoal,
                DeadLine = model.DeadLine,
                History = model.History,
                BankAccount = model.BankAccount,
                CategoryId = model.CategoryId,
                StudentId = student!.Id
            };
            var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
            var fileExtension = Path.GetExtension(model.Image.FileName);
            fileName = $"{fileName}_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.{fileExtension}";
            project.Image = fileName;
            try
            {
                await _context.Projects.AddAsync(project);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "projects", fileName);
                await using var stream = new FileStream(path, FileMode.Create);
                await model.Image.CopyToAsync(stream);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado");
                return View(model);
            }
        }

        return RedirectToAction("Index", "Home");
    }
}