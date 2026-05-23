using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LojaApi.Auth;
namespace LojaApi.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
// Chave do token
private const string chaveJwt =
"MINHA_CHAVE_SUPER_SECRETA_123456";
// ====================================
// LOGIN
// ====================================
[HttpPost("login")]
public IActionResult Login(Login login)
{
// Verifica usuário e senha
if (login.Usuario == "admin" &&
login.Senha == "123")
{
// Cria manipulador do token
var tokenHandler = new JwtSecurityTokenHandler();
// Converte chave para bytes
var key = Encoding.UTF8.GetBytes(chaveJwt);
// Configura token
var tokenDescriptor =
new SecurityTokenDescriptor
{
// Dados do usuário
Subject = new ClaimsIdentity(new[]
{
new Claim(ClaimTypes.Name,
login.Usuario)
}),
// Expiração
Expires = DateTime.UtcNow.AddHours(1),
// Assinatura do token
SigningCredentials =
new SigningCredentials(
new SymmetricSecurityKey(key),
SecurityAlgorithms.HmacSha256Signature
)
};
// Gera token
var token =
tokenHandler.CreateToken(tokenDescriptor);
// Converte token para texto
var tokenString =
tokenHandler.WriteToken(token);
// Retorna token
return Ok(new
{
token = tokenString
});
}
// Login inválido
return Unauthorized("Usuário ou senha inválidos");
}
}
}