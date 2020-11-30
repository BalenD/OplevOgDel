using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OplevOgDel.Web.Controllers.Base;
using OplevOgDel.Web.Models.Configuration;
using OplevOgDel.Web.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Controllers
{
    public class UserController : Controller
    {
        private ApiUrls _ApiUrls;

        public UserController(IOptions<ApiUrls> apiUrls)
        {
            _ApiUrls = apiUrls.Value;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {
            if (user.Username == null || user.Password == null)
            {
                ViewBag.Message = "Udfyld venligst begge felter!";
                return View();
            }

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(_ApiUrls.API + _ApiUrls.Login, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TokenDto token = JsonConvert.DeserializeObject<TokenDto>(result);
                    var claims = new List<Claim>()
                    {
                        new Claim("access_token", token.jwt)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return Redirect("/Home/Index");
                }
            }

            ViewBag.Message = "Forkert brugernavn eller kodeord";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            if (user.Username == null || user.Password == null)
            {
                ViewBag.Message = "Udfyld venligst begge felter!";
                return View();
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_ApiUrls.API + _ApiUrls.Login);
                if (response.IsSuccessStatusCode)
                {

                    return Redirect("/User/Login");
                }
            }

            ViewBag.Message = "Forkert brugernavn eller kodeord";
            return View();
        }
    }
}
