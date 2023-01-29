using CookiesAuthApp.BAL.Interfaces;
using CookiesAuthApp.BAL.ModelsDTO;
using CookiesAuthApp.WEB.Common;
using CookiesAuthApp.WEB.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookiesAuthApp.WEB.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        IUserServices userServices;

        public AuthController(IUserServices _userServices)
        {
            userServices = _userServices;
        }

        //HttpPost Register
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterVM registerVM)
        {
            //Проверка: Существует ли уже такой пользователь(Login)
            var user = userServices.GetByLogin(registerVM.Login);
            if (user != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<RegisterVM> { StatusCode = "Error", Message = "Пользователь с таким Лоигном уже зарегестрирован" });

            //Проверка: совпадают ли введеные пароли
            if (registerVM.Password != registerVM.ConfirmPassword)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<RegisterVM> { StatusCode = "Error", Message = "Пароли не совпадают" });


            UserDTO userDTO = new()
            {
                Name = registerVM.Name,
                Login = registerVM.Login,
                Password = registerVM.Password,
                Role = "user"
            };
            //Создание пользователя
            var result = userServices.Create(userDTO);
            //Проверка: Успешно создан ли пользователь
            if (result != null)
                return StatusCode(StatusCodes.Status200OK,
                    new Response<RegisterVM> { StatusCode = "OK", Message = "Пользователь успешно зарегестрирован" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<RegisterVM> { StatusCode = "Error", Message = "При регистрации возникла ошибка!" });

        }
        //HttpPost Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            var user = userServices.GetByLogin(loginVM.Login);
            if (user == null || loginVM.Password != user.Password)
                return StatusCode(StatusCodes.Status401Unauthorized,
                    new Response<LoginVM> { StatusCode = "Error", Message = "Неверный логин и/или парроль" });

            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Login) ,
                new Claim(ClaimTypes.Role, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return StatusCode(StatusCodes.Status200OK,
                new Response<LoginVM> { StatusCode = "OK", Message = "Успешная авторизация" });

        }

        //HttpGet LogOut
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return StatusCode(StatusCodes.Status200OK);
        }
        //HttpGet GetAll Authorize
        [HttpGet]
        [Authorize]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var data = userServices.GetAll();
            return StatusCode(StatusCodes.Status200OK, new Response<IEnumerable<UserDTO>> { StatusCode = "OK", Data = data });
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var data = userServices.GetAll();
            return StatusCode(StatusCodes.Status200OK, new Response<IEnumerable<UserDTO>> { StatusCode = "OK", Data = data });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var data = userServices.GetAll();
            return StatusCode(StatusCodes.Status200OK, new Response<IEnumerable<UserDTO>> { StatusCode = "OK", Data = data });
        }

        //HttpGet GetAll NonAuthorize
        [HttpGet]
        [Route("GetAllNonAuth")]
        public IActionResult GetAllNonAuth()
        {
            var data = userServices.GetAll();
            Redirect("/login");
            return StatusCode(StatusCodes.Status200OK, new Response<IEnumerable<UserDTO>> { StatusCode = "OK", Data = data });
        }

        [HttpGet]
        [Route("GetRole")]
        public string GetRole() 
        {
            var user = HttpContext.User.Identity;
            var role = HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            if (user.IsAuthenticated)
                return $"Авторизован: Name: {user.Name}, Role: {role}";
            else
                return "Не вторизован";
        }
    }
}
