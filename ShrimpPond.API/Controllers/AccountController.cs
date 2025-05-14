using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using ShrimpPond.API.Authorization.Models;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
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
        private readonly IGmailSender _gmailSender;
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IGmailSender gmailSender, IMemoryCache cache, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _gmailSender = gmailSender;
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            // Kiểm tra email đã tồn tại
            var existing = await _userManager.FindByEmailAsync(model.Email);
            if (existing != null)
            {
                throw new BadHttpRequestException("Email đã được sử dụng.");
            }

            // Tạo OTP và khóa cache
            var otp = new Random().Next(100000, 999999).ToString();
            var cacheKey = Guid.NewGuid().ToString(); // Khóa duy nhất cho mỗi yêu cầu đăng ký

            // Dữ liệu lưu vào cache
            var registrationData = new
            {
                Otp = otp,
                Email = model.Email,
                Password = model.Password
            };

            // Lưu vào cache với thời gian hết hạn 10 phút
            _cache.Set(cacheKey, registrationData, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            try
            {
                // Gửi email OTP
                await _gmailSender.SendConfirmationEmailAsync(new GmailMessage()
                {
                    To = model.Email,
                    Body = otp,
                    Subject = "Mã OTP xác nhận"
                });

                // Trả về cacheKey để frontend gửi lại trong VerifyEmail
                return Ok(new { message = "Vui lòng kiểm tra email để lấy mã OTP.", cacheKey });
            }
            catch (Exception ex)
            {
                // Xóa cache nếu gửi email thất bại
                _cache.Remove(cacheKey);
                return StatusCode(500, new { error = "Không thể gửi email OTP, vui lòng thử lại.", details = ex.Message });
            }
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string OtpCode, [FromQuery] string cacheKey)
        {
            // Lấy dữ liệu từ cache
            if (!_cache.TryGetValue(cacheKey, out var cachedData))
            {
                return BadRequest(new { error = "Phiên đăng ký đã hết hạn hoặc không tồn tại, vui lòng đăng ký lại." });
            }

            var registrationData = (dynamic)cachedData;

            // Kiểm tra OTP và email
            if (OtpCode != registrationData.Otp || email != registrationData.Email)
            {
                throw new BadHttpRequestException("Mã OTP không chính xác");
            }

            // Tạo user
            var user = new IdentityUser
            {
                UserName = email,
                Email = registrationData.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registrationData.Password);

            if (result.Succeeded)
            {
                // Xóa cache sau khi đăng ký thành công
                _cache.Remove(cacheKey);
                return Ok(new { message = "Đăng ký thành công!" });
            }

            // Xóa cache nếu đăng ký thất bại
            return Ok("" );
        }

        #region sendlink

        //[HttpPost("ConfirmEmail")]
        //public async Task<IActionResult> ConfirmEmail(string token, string username)
        //{
        //    var user = await _userManager.FindByNameAsync(username);

        //    if(user != null)
        //    {

        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return Ok(new { message = "Email Verified Sucessfully" });
        //        }
        //    }
        //    return BadRequest("This User Doesnot Exits!");
        //}


        //public async Task<IActionResult> ConfirmEmail(string token, string username)
        //{
        //    var user = await _userManager.FindByNameAsync(username);

        //    if (user != null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            ViewBag.Message = "Email đã được xác minh thành công!"; // Truyền thông báo tới View
        //            return View("ConfirmEmailSuccess"); // Trả về View ConfirmEmailSuccess.cshtml
        //        }
        //        else
        //        {
        //            // Xử lý trường hợp xác nhận thất bại (ví dụ: token không hợp lệ)
        //            // Bạn có thể muốn log các lỗi chi tiết từ result.Errors
        //            ViewBag.Message = "Không thể xác minh email. Vui lòng thử lại hoặc liên hệ hỗ trợ.";
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description); // Thêm lỗi vào ModelState để hiển thị chi tiết hơn nếu muốn
        //            }
        //            return View("ConfirmEmailFailure"); // Trả về View ConfirmEmailFailure.cshtml
        //        }
        //    }

        //    ViewBag.ErrorMessage = "Người dùng này không tồn tại!"; // Truyền thông báo lỗi tới View
        //    return View("UserNotFound"); // Trả về View UserNotFound.cshtml (hoặc một view lỗi chung)
        //                                 // Hoặc bạn có thể trả về một trang lỗi chuẩn của ASP.NET Core:
        //                                 // return NotFound("Người dùng này không tồn tại!");
        //                                 // Hoặc một View cụ thể cho lỗi 404:
        //                                 // return View("NotFound");
        //}

        #endregion

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
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
