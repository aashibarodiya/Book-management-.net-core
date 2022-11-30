using BooksWeb.Utils;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksWeb.Controllers
{
    [ApiController]
    [Route("/api/users")]
    [ExceptionMapper(ExceptionType = typeof(InvalidIdException), StatusCode = 404)]

    public class UserApiController : Controller
    {
        IUserService userService;
        IConfiguration configuration;
        private object _configuration;

        public UserApiController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var info = await userService.Register(user);

            return Created("", info);
        }

        [HttpPost("login")]
        [ExceptionMapper(ExceptionType = typeof(InvalidCredentialsException), StatusCode = 401)]
        public async Task<IActionResult> Login([FromBody] LoginInfo info)
        {
            var user = await userService.Login(info.Email, info.Password);

            var roles = "";
            foreach (var role in user.Roles)
            {
                roles += $"{role},";
            }
            if (roles.Length > 0)
                roles = roles.Substring(0, roles.Length - 1); //remove last comma

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim("Roles", roles),
           };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                user = user

            });
        }









        [HttpPost("userroles")]
        public async Task<IActionResult> AssignRoles(RolesInfo info)
        {

            foreach (var role in info.Roles)
            {
                await userService.AssignRoleToUser(info.Email, role);
            }

            var user = await userService.GetUserByEmail(info.Email);

            return Ok(user);
        }

        [HttpPost("roles")]
        [ExceptionMapper(ExceptionType = typeof(DbUpdateException), StatusCode = 400, Message = "Duplicate Role")]
        public async Task<IActionResult> AddRole(Role role)
        {
            var info = await userService.CreateRole(role);
            return Created("", info);

        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await userService.GetAllRoles());
        }

        /*
         * Returns User Name and email if found
         * HTTP 404 otherwise
         */
        [HttpGet("validate/{email}")]
        public async Task<IActionResult> Validate(string email)
        {
            var user = await userService.GetUserByEmail(email);
            return Ok(new { email = user.Email, name = user.Name });
        }

        [HttpPost("favoriteBooks")]
        public async Task<IActionResult> AddFavoriteBook(FavoriteBook book)
        {
            var result = await userService.AddFavoriteBook(book);

            return Created("", result);

        }

        [HttpGet("{userId}/favoriteBooks")]
        public async Task<IActionResult> GetUserFavoriteBooks(string userId)
        {
            return Ok(await userService.GetAllFavoriteBooks(userId));
        }
    }
}
