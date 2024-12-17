using api_painel_producao.Models;
using api_painel_producao.Utils;
using api_painel_producao.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_painel_producao.Services {
    public interface IAuthService {
        string CreateToken (User user);
        Task<UserInfo> ExtractTokenInfo (string token);
    }


    public class AuthService : IAuthService {

        private readonly IUserRepository _userRepository;

        public AuthService (IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public string CreateToken (User user) {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor {

                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim("username", user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserInfo> ExtractTokenInfo (string token) {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            ClaimsPrincipal info = null;

            try {
                info = tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                }, out var validatedToken);

            } catch (Exception e) {
                return null;
            }

            var tokenUser = await _userRepository.FindUserByUsernameAsync(info.FindFirst("username")?.Value);

            if (tokenUser is null)
                return null;

            var userInfo = new UserInfo {
                Id = tokenUser.Id,
                Name = tokenUser.Name,
                Email = tokenUser.Email,
                Username = tokenUser.Username,
                Role = tokenUser.Role,
                CreatedAt = tokenUser.CreatedAt,
                LastModifiedAt = tokenUser.DataLastModifiedAt
            };
            
            return userInfo;
        }
    }
}
