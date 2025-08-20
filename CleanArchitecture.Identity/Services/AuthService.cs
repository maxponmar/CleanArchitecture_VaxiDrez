namespace CleanArchitecture.Identity.Services;

public class AuthService(
    UserManager<ApplicationUser> usermanager,
    SignInManager<ApplicationUser> signInManager,
    JwtSettings jwtSettings)
    : IAuthService
{
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await usermanager.FindByEmailAsync(request.Email);
        if(user == null || user.UserName == null)
            throw new Exception($"User {request.Email} not found");

        var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
        if(!result.Succeeded)
            throw new Exception("Invalid credentials");
    
        var token = await GenerateToken(user);
        var authResponse = new AuthResponse()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            Username = user.UserName
        };
        
        return authResponse;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await usermanager.FindByNameAsync(request.Username);
        if(existingUser != null)
            throw new Exception($"User {request.Email} already exists");
        
        var existingEmail = await usermanager.FindByEmailAsync(request.Email);
        if(existingEmail != null)
            throw new Exception($"Email {request.Email} already exists");
        
        var user = new ApplicationUser()
        {
            UserName = request.Username,
            Email = request.Email,
            Nombre = request.Nombre,
            Apellidos = request.Apellidos,
            EmailConfirmed = true
        };
        
        var result = await usermanager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await usermanager.AddToRoleAsync(user, "Operator");
            var token = await GenerateToken(user);
            return new RegistrationResponse()
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                UserId = user.Id
            };
        }
        
        throw new Exception($"Error creating user: {result.Errors}");
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await usermanager.GetClaimsAsync(user);
        var roles = await usermanager.GetRolesAsync(user);
        
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
            roleClaims.Add(new Claim(ClaimTypes.Role, role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(CustomClaimTypes.Uid, user.Id),
        }.Union(userClaims).Union(roleClaims);
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.ExpirationInMinutes),
            signingCredentials: signingCredentials);
        
        return jwtSecurityToken;
    }
}