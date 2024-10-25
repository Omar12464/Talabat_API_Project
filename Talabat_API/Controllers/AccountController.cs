using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_API.Helper;
using Talabat_Core.Models.Identity;
using Talabat_Core.ServiceInterfaces;

namespace Talabat_API.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userMnager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userMnager,SignInManager<AppUser> signInManager,IAuthService authService,IMapper mapper)
        {
            _userMnager = userMnager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var dd =await _userMnager.FindByEmailAsync(login.Email.ToLower());
            if(dd == null) return Unauthorized(new APIResponse(401));
            var result =await _signInManager.CheckPasswordSignInAsync(dd, login.Password, false);
            if (result.Succeeded is false) { return Unauthorized(new APIResponse(401)); }
            else
            {
                return Ok(new UserDTO()
                {
                    DisplayName = dd.DisplayName,
                    Email = dd.Email,
                    Token = await _authService.CreateTokenAsync(dd,_userMnager)
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
                Token = await _authService.CreateTokenAsync(user, _userMnager)
            });

        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email=User.FindFirstValue(ClaimTypes.Email)??string.Empty;
            var user=await _userMnager.FindByEmailAsync(email.ToLower());
            return Ok(new UserDTO()
            {
                DisplayName=user.DisplayName??string.Empty,
                Email=user.Email??string.Empty,
                Token=await _authService.CreateTokenAsync(user,_userMnager)
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]
        public async Task<ActionResult<AdressDtoo>> GetAddress()
        {
            var user = await _userMnager.FindUserWithAddressByEmailAsync(User);
            var map = _mapper.Map<Address, AdressDtoo>(user.Address);
            return Ok(map);
        }

    }
}
