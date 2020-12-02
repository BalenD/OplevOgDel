using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OplevOgDel.Web.Controllers.Base;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.Configuration;
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;
using OplevOgDel.Web.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Controllers
{
    public class UserController : Controller
    {
        private ApiUrls _apiUrls;
        private readonly OplevOgDelService _oplevOgDelService;

        public UserController(IOptions<ApiUrls> apiUrls, OplevOgDelService oplevOgDelService)
        {
            _apiUrls = apiUrls.Value;
            _oplevOgDelService = oplevOgDelService;
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

                HttpResponseMessage response = await client.PostAsync(_apiUrls.API + _apiUrls.Login, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TokenDto token = JsonConvert.DeserializeObject<TokenDto>(result);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.ReadToken(token.jwt) as JwtSecurityToken;
                    var role = securityToken.Claims.First(c => c.Type == "role").Value;
                    var claims = new List<Claim>()
                    {
                        new Claim(UserClaims.AccessToken, token.jwt),
                        new Claim(ClaimTypes.NameIdentifier, securityToken.Claims.First(c => c.Type == "userId").Value),
                        new Claim(ClaimTypes.Role, role)
                    };

                    if (role == Roles.User)
                    {
                        claims.AddRange(new Claim[] {
                            new Claim(UserClaims.ProfileId, securityToken.Claims.First(c => c.Type == UserClaims.ProfileId).Value),
                            new Claim(ClaimTypes.Name, securityToken.Claims.First(c => c.Type == UserClaims.FirstName).Value)
                        });
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return Redirect("/");
                }
            }

            ViewBag.Message = "Forkert brugernavn eller kodeord";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(_apiUrls.API + _apiUrls.Register, content);
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("/User/Login");
                } 
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = "Brugernavn eksisterer allerede";
                    return View();
                } 
                ViewBag.Message = "Fejl med server, kontakt sidens administrator";
                return View();
            }
        }

        //[Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Profile()
        {
            string endPoint = _apiUrls.Profiles + "/9600bf95-bf37-4e6d-aeed-53d84a96a205";

            ProfileViewModel viewModel = new ProfileViewModel();

            HttpResponseMessage response = await _oplevOgDelService.Client.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                viewModel.Profile = await response.Content.ReadAsAsync<ProfileDto>();
                if (viewModel.Profile.ListOfExps.Count != 0)
                {
                    viewModel.SelectedListOfExps = viewModel.Profile.ListOfExps[0];
                    viewModel.ListOfListOfExps = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(viewModel.Profile.ListOfExps, "Id", "Name");
                }

                return View(viewModel);
            }

            return Redirect("/");
        }
    }
}
