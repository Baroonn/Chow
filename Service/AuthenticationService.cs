using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token) 
        { 
            var jwtSettings = _configuration.GetSection("JwtSettings"); 
            var tokenValidationParameters = new TokenValidationParameters 
            { 
                ValidateAudience = true, 
                ValidateIssuer = true, 
                ValidateIssuerSigningKey = true, 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CHOWSECRET"))), 
                ValidateLifetime = true, ValidIssuer = jwtSettings["validIssuer"], 
                ValidAudience = jwtSettings["validAudience"] 
            }; 
            var tokenHandler = new JwtSecurityTokenHandler(); 
            SecurityToken securityToken; 
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken); 
            var jwtSecurityToken = securityToken as JwtSecurityToken; 
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) 
            { 
                throw new SecurityTokenException("Invalid token"); 
            } 
            return principal; 
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CHOWSECRET"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken(); 
            _user.RefreshToken = refreshToken; 
            if (populateExp) _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); 
            await _userManager.UpdateAsync(_user); 
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions); 
            return new TokenDto(accessToken, refreshToken);
        }

        public async Task<IdentityResult> RegisterUser(UserRegistrationDto userRegistrationDto)
        {
            var user = _mapper.Map<User>(userRegistrationDto);
            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userRegistrationDto.Roles);
            }
            return result;
        }

        public async Task<bool> ValidateUser(UserAuthenticationDto userAuthDto)
        {
            _user = await _userManager.FindByNameAsync(userAuthDto.UserName);
            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userAuthDto.Password));
            if (!result)
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong Username or Password.");
            }
            return result;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto) 
        { 
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken); 
            var user = await _userManager.FindByNameAsync(principal.Identity.Name); 
            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) 
                throw new RefreshTokenBadRequest(); 
            _user = user; 
            return await CreateToken(populateExp: false); }
    }
}
