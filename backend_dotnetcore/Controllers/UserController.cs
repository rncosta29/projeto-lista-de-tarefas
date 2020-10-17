using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend_dotnetcore.Data;
using backend_dotnetcore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace backend_dotnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(
            IRepository repo, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _repo = repo;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    _repo.Add(model);

                    if(await _repo.SaveChangesAsync())
                    {
                        return Ok(BuildToken(model));
                    }
                }
                else
                {
                    return BadRequest("Usuário ou senha inválidos");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest("Erro não esperado!!!");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "login Inválido.");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("filter/cpf/{cpf}")]
        public async Task<ActionResult> GetPorCpf(long cpf)
        {
            try
            {
                var result = await _repo.GetUserByCpf(cpf);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("filter/id/{id}")]
        public async Task<IActionResult> GetPorId(string id)
        {
            try
            {
                var result = await _repo.GetUserById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        private UserToken BuildToken(UserModel model)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim("test", "A vid4 & 83L4"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do token
            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
