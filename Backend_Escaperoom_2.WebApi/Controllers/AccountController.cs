using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Web;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SBackend_Escaperoom_2.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : Controller
    {
        /*Atributos*/
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly LanguagesHelper _languagesHelper;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger,
            LanguagesHelper languagesHelper)
        {
            _accountService = accountService;
            _languagesHelper = languagesHelper;
            _logger = logger;
        }

        //Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            this._logger.LogInformation("GET Logout");

            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginRequest());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            this._logger.LogInformation("GET Login Web");

            try
            {
                if (ModelState.IsValid)
                {
                    //Usuario
                    var user = await _accountService.GetUserByEmail(request.Email.ToLower());
                    if (user == null)
                    {
                        throw new Exception(this._languagesHelper.LoginEmailNoExist);
                    }

                    if (!user.EstadoUsuario)
                    {
                        throw new Exception(this._languagesHelper.UserNoActivo);
                    }

                    if (!user.EmailConfirmed)
                    {
                        throw new Exception(this._languagesHelper.LoginEmailConfim);
                    }

                    var roles = await _accountService.GetRolesByUserAsync(user);
                    if (!roles.Contains(TiposUsuarios.Desarrollador.ToString()))
                    {
                        throw new Exception(this._languagesHelper.NoAccessLogin);
                    }

                    var res = await _accountService.ValidatePasswordAsync(user.UserName.ToLower(), request.Password, request.RememberMe);
                    if (res.Succeeded)
                    {
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(Request.Query["ReturnUrl"].First());
                        }

                        return RedirectToAction("Index", "Home");
                    }

                    throw new Exception(this._languagesHelper.LoginPassIncorrect);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, e.Message);
            }

            return View(request);
        }
    }
}
