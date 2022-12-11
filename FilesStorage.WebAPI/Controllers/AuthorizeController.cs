using FilesStorage.WebAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilesStorage.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthorizeController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizeController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
      return "AuthorizeController :: Acessado em : " + DateTime.Now.ToLongDateString();
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserDTO model)
    {
      var user = new IdentityUser
      {
        UserName = model.Email,
        Email = model.Email,
        EmailConfirmed = true,
      };

      var result = await _userManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
        return BadRequest(result.Errors);
      }

      await _signInManager.SignInAsync(user, false);
      return Ok(GenerateToken(model));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserDTO userInfo)
    {
      var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

      if (result.Succeeded)
      {
        return Ok(GenerateToken(userInfo));
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Login inválido!");
        return BadRequest(ModelState);
      }
    }

    private UserToken GenerateToken(UserDTO userInfo)
    {
      //define declarações do usuário
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        new Claim("meuPet", "negrinha"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      };

      //gera uma chave com base em um algoritmo simétrico
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

      //gera a assinatura digital do token usando o algoritmo HMAC e a chave privada
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      //tempo de expiração do token
      var expireHours = _configuration["TokenConfiguration:ExpireHours"];
      var expiration = DateTime.UtcNow.AddHours(double.Parse(expireHours));

      //geração do token JWT
      JwtSecurityToken token = new JwtSecurityToken(
        issuer: _configuration["TokenConfiguration:Issuer"],
        audience: _configuration["TokenConfiguration:Audience"],
        claims: claims,
        expires: expiration,
        signingCredentials: credentials
        );

      //retorna o token
      return new UserToken()
      {
        Authenticated = true,
        Token = new JwtSecurityTokenHandler().WriteToken(token),
        Expiration = expiration,
        Message = "Token JWT Ok"
      };
    }
  }
}