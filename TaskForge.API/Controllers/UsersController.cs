using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskForge.Application.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Infrastructure.Persistence;

namespace TaskForge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase //gives you access to helper methods like Ok(), NotFound(), BadRequest().
    {
        //Since you’ll need to save users into DB, you need a TaskForgeDbContext inside this controller.
        private readonly TaskForgeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        // _config lets you read things from appsettings.json(like JWT secret, issuer, audience).
        private readonly IConfiguration _config;
        public UsersController(TaskForgeDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IConfiguration config)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerDto)
        {
            //anyasync is used to find atleast one entry if found then true otherwise false
            if(await _dbContext.Users.AnyAsync(u => u.UserName == registerDto.UserName ))
            {
                return BadRequest("Email already registered");
            }
            var userEntity = _mapper.Map<User>(registerDto);
            userEntity.PasswordHash = _passwordHasher.HashPassword(userEntity, registerDto.Password);
            _dbContext.Users.Add(userEntity);
            await _dbContext.SaveChangesAsync();
            return Ok(_mapper.Map<UserDto>(userEntity));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName || u.Email == loginDto.UserName);

            if (user == null)
            {
                return NotFound("User Not found!");
            }

            //verify hash password
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password");

            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
               
                };
                // For each role assigned to the user, add a Role claim
                foreach (var userRole in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
                var secretKey = _config["JwtSettings:Secret"];
                var issuer = _config["JwtSettings:Issuer"];
                var audience = _config["JwtSettings:Audience"];
                if (!int.TryParse(_config["JwtSettings:ExpiryMinutes"], out int expiryMinutes))
                {
                    expiryMinutes = 60; // fallback default value
                }


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );
                var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new {token= jwtTokenString, user = _mapper.Map<UserDto>(user) });
            }
        }


    }
}
