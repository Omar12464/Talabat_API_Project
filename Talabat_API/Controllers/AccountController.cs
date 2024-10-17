using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_Core.Models.Identity;

namespace Talabat_API.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userMnager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userMnager,SignInManager<AppUser> signInManager)
        {
            _userMnager = userMnager;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var dd =await _userMnager.FindByEmailAsync(login.Email);
            if(dd == null) return Unauthorized(new APIResponse(401));
            var result =await _signInManager.CheckPasswordSignInAsync(dd, login.Password, false);
            if (result.Succeeded is false) { return Unauthorized(new APIResponse(401)); }
            else
            {
                return Ok(new UserDTO()
                {
                    DisplayName = dd.DisplayName,
                    Email = dd.Email,
                    Token = "this will be token"
                });
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            //create User
            var user = new AppUser() {
                DisplayName=register.DisplayName,Email=register.Email ,
                UserName = register.Email.Split("@")[0] ,
                PhoneNumber = register.PhoneNumber ,
                
            };
            //createasync
            var result=await _userMnager.CreateAsync(user,register.Password);
            //return
            if (result.Succeeded is false) return BadRequest(new APIResponse(400));
            else return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "this will be token"
            });

        }

    }
}
