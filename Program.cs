using LojaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(
builder.Configuration.GetConnectionString("DefaultConnection"),
ServerVersion.AutoDetect(
builder.Configuration.GetConnectionString("DefaultConnection")
)
)
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	var securityScheme = new OpenApiSecurityScheme
	{
		Description = "Informe 'Bearer' seguido do token JWT. Exemplo: Bearer eyJhbGciOiJIUzI1NiIs...",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT"
	};

	c.AddSecurityDefinition("Bearer", securityScheme);

	c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecuritySchemeReference("Bearer", document, null),
			new List<string>()
		}
	});
});

// Chave secreta do token
var chaveJwt = "MINHA_CHAVE_SUPER_SECRETA_123456";

// Configura autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
// Valida chave
ValidateIssuerSigningKey = true,
// Define chave secreta
IssuerSigningKey =
new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(chaveJwt)
),
// Não valida servidor
ValidateIssuer = false,
// Não valida cliente
ValidateAudience = false
};
});
// Adiciona autorização
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();