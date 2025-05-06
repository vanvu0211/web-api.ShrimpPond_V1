using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShrimpPond.API.Authorization.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShrimpPond.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var result1 = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded && result1.Succeeded)
            {
                return Ok(new { message = "User Registed Sucessfully" });
            }
            

            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));


                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["JWT:ValidIssuer"],
                        expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT:ExpiryMinutes"]!)),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                        )
                    );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }

        [HttpPost("Add-Role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            //Kiểm tra role đã có chưa
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Add Role Sucessfully" });
                }
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "Role already exits" });
        }

        [HttpPost("Assign-Role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Assign Role Sucessfully" });
            }
            return BadRequest(result.Errors);

        }
    }
}
