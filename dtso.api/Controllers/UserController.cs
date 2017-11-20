using dtso.api.Models.Responses;
using dtso.auth.Enum;
using dtso.auth.Logic;
using dtso.auth.Logic.Interfaces;
using dtso.auth.Models.Buisness;
using dtso.auth.Models.DataTransfer.Form;
using dtso.auth.Settings;
using dtso.data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        IUserRegistrar _registrar;
        IOptions<AuthSettings> _settings;
        TokenManager _tokenManager;

        public UserController(IUserRegistrar userRegist, IOptions<AuthSettings> settings, TokenManager tokenMan)
        {
            _registrar = userRegist;
            _settings = settings;
            _tokenManager = tokenMan;
        }


       


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register([FromBody] UserRegistrationForm registration)
        {
            var registrationStatus = _registrar.RegisterUser(registration);

            if (registrationStatus == AuthStatus.InvalidPassword)
                return BadRequest("The provided password is in an invalid format");

            return Login(new LoginCredentialsForm { Email = registration.Email, Passphrase = registration.Passphrase });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentialsForm credentials)
        {
            var user = _registrar.ValidateLogin(credentials);

            if (user == null)
                return BadRequest("Incorect Username or Password");
            
            var response = _tokenManager.CreateToken(user);

            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpPost("edit")]
        [Authorize]
        public IActionResult EditUser([FromBody] UserRegistrationForm form)
        {
            var userInfo = _tokenManager.ExtractUserInformation(User.Claims);

            if(userInfo.Permissions != Permissions.ADMIN)
            {
                //If not admin, permissions cannot be changed. We use negative 1 to signify this.
                form.Permissions = -1;

                if (userInfo.UserId != form.UserId)
                {
                    return BadRequest("You cannot modify this user.");
                }
            }

            var userId = _registrar.EditUser(form);

            if(userId == 0)
            {
                return BadRequest("Invalid Password");
            }

            return Ok(UserBasics.MapFromEntity(_registrar.GetUser(userId)));
        }

        [HttpGet("listing")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserListing()
        {
            var users = _registrar.GetUserListing();

            var response = new List<UserBasics>();
            foreach(var user in users)
            {
                response.Add(UserBasics.MapFromEntity(user));
            }

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUser()
        {
            var userInfo = _tokenManager.ExtractUserInformation(User.Claims);
            var user = _registrar.GetUser(userInfo.UserId);


            return Ok(UserBasics.MapFromEntity(user));
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUser(int userId)
        {
            var user = _registrar.GetUser(userId);
            
            return Ok(UserBasics.MapFromEntity(user));
        }

    }
}
