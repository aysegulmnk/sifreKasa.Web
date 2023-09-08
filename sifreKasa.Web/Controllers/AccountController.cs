using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using sifreKasa.Data.Context;
using sifreKasa.Data.Data;
using sifreKasa.Web.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace sifreKasa.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly sifreKasaDB _dbContext;
        private readonly IConfiguration _config;

        public AccountController(sifreKasaDB dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string hashedPassword = HashPassword(model.Password);

                    Users user = new Users
                    {
                        UserName = model.Username,
                        PasswordHash = hashedPassword,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };

                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Login");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                // Veritabanı hatasını kaydet veya işleyebilirsiniz
                ModelState.AddModelError(string.Empty, "Kayıt işlemi sırasında bir hata oluştu. Hata detayı: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserName == model.Username);

                if (user != null)
                {
                 
                    string hashedPassword = HashPassword(model.Password);
                    if (user.PasswordHash == hashedPassword)
                    {
                        
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
