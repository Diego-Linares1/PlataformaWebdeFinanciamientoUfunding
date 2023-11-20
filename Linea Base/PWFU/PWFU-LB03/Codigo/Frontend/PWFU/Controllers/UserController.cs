using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PWFU.DAL;
using PWFU.Models;
using PWFU.ViewModels;

namespace PWFU.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UserController(ApplicationDbContext context, SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpUser model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError("", "El email ya se encuentra en uso");
                return View(model);
            }

            user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.IsStudent)
                    {
                        result = await _userManager.AddToRoleAsync(user, "Estudiante");

                        if (result.Succeeded)
                        {
                            var createdUser = await _userManager.FindByEmailAsync(model.Email);
                            var studentInfo = new StudentInfo()
                            {
                                Semester = (int)model.Semester!,
                                YearOfEntry = (int)model.YearOfEntry!,
                                University = model.University!,
                                Speciality = model.Speciality!,
                                StudentId = createdUser.Id
                            };

                            _context.StudentInfos.Add(studentInfo);

                        }
                    }

                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, false);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado");
            }
        }
        return RedirectToAction("Index", "Home");
    }


    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInUser model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email o contraseña incorrectos");
        }
        return View(model);
    }
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return RedirectToAction("Index", "Home");
    }
}