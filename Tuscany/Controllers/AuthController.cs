using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tuscany.Utility;
using Tuscany.DataAccess.DB;
using Tuscany.Models;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.DataAccess.Repository;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        public UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public UnitOfWork _unitOfWork;

        public AuthController(UnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        [Route("/postUsername")]
        public string postUsername(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            // Creating JWT token
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost]
        [Route("/regUser")]
        public async Task<IActionResult> RegUser([FromBody] Register model)
        {
            var useEx = await _userManager.FindByNameAsync(model.UserName!);
            if (useEx is not null)
                return StatusCode(StatusCodes.Status500InternalServerError, "User in db already");


            User user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                Avatar = DefaultAvatars.GetRandomImg(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var res = await _userManager.CreateAsync(user, model.Password!);
            if (!res.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, res.Errors);

            await SetRole(model.UserName, UserRoles.User);

            return Content("Success");
        }

        [HttpPost]
        [Route("/setAdmin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> SetRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is not null)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await _roleManager.RoleExistsAsync(role))
                    await _userManager.AddToRoleAsync(user, role);

                return Ok("Role added");
            }
            return StatusCode(404);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = AuthHelper.GetToken(authClaims, user.UserName!);

                return Ok(new
                {
                    token.UserName,
                    token.Token,
                    token.Expires
                });
            }
            return Unauthorized();
        }

        //[HttpDelete]
        //public IActionResult DeleteUser(string id)
        //{

        //    List<IdentityUserClaim<string>> claims = _unitOfWork._db.UserClaims
        //        .Where(x => x.UserId == id).ToList();

        //    _unitOfWork._db.UserClaims.RemoveRange(claims);

        //    List<IdentityUserLogin<string>> userLogins = _unitOfWork._db.UserLogins
        //        .Where(x => x.UserId == id).ToList();

        //    _unitOfWork._db.UserLogins.RemoveRange(userLogins);




        //    _unitOfWork.Save();

        //    return Ok();
        //}
    }
}

