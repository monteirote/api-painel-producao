using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Repositories;
using api_painel_producao.Models;

namespace api_painel_producao.Services {

    public interface IAccountService {
        Task<ServiceResponse<int>> CreateUserAsync (UserSignupViewModel user);
        Task<ServiceResponse<string>> LoginAsync (UserLoginViewModel user);
        Task<ServiceResponse<string>> DeactivateUserAsync (string token, int userId);
    }


    public class AccountService : IAccountService {

        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public AccountService (IUserRepository repository, IAuthService authService) {
            _repository = repository;
            _authService = authService;
        }

        public async Task<ServiceResponse<int>> CreateUserAsync (UserSignupViewModel userData) {
            try {
                var parts = GenerateHash(userData.Password).Split(':');

                var userToAdd = new User {
                    Name = userData.Name,
                    Email = userData.Email,
                    Username = userData.Username,
                    PasswordSalt = parts[0],
                    PasswordHash = parts[1],
                    Role = Models.Enums.UserRole.Admin,
                };

                await _repository.CreateAsync(userToAdd);

                var response = new ServiceResponse<int> { Success = true, Message = "User created sucessfully!", Data = userToAdd.Id };

                return response;
            } catch (Exception e) {
                return new ServiceResponse<int> { Success = false, Message = "Error creating user: " + e.Message };
            }
        }

        public async Task<ServiceResponse<string>> LoginAsync (UserLoginViewModel userData) {
            try {
                var foundUser = await _repository.GetByUsernameAsync(userData.Username);

                string errorMessage = "";

                if (foundUser is null)
                    errorMessage = "Login failed: incorrect username or password.";

                if (!VerifyPassword(userData.Password, foundUser.PasswordSalt, foundUser.PasswordHash))
                    errorMessage = "Login failed: incorrect username or password.";

                if (!foundUser.IsActive)
                    errorMessage = "Login failed: your account has been deactivated.";


                if (errorMessage != "")
                    return new ServiceResponse<string> { Success = false, Message = errorMessage };

                var token = _authService.CreateToken(foundUser);

                return new ServiceResponse<string> { Success = true, Message = "Login successful.", Data = token };

            } catch (Exception e) {
                return new ServiceResponse<string> { Success = false, Message = "Login failed: " + e.Message };
            }
        }

        public async Task<ServiceResponse<string>> DeactivateUserAsync (string token, int userId) {

            var tokenUser = await _authService.ExtractTokenInfo(token);

            if (tokenUser is null)
                return new ServiceResponse<string> { Success = false, Message = "Action failed: This token is not valid." };


            var userToDeactivate = await _repository.GetByIdAsync(userId);

            if (userToDeactivate is null)
                return new ServiceResponse<string> { Success = false, Message = "Action failed: This user does not exist." };

            if (tokenUser.Role.ToString() != "Admin" && tokenUser.Id != userId)
                return new ServiceResponse<string> { Success = false, Message = "Action failed: You do not have the required permissions." };


            await _repository.DeactivateUserAsync(userToDeactivate);

            return new ServiceResponse<string> { Success = true, Message = "User has been successfully deactivated." };
        }

        public async Task<ServiceResponse<string>> ChangePasswordAsync (string token, int userId, string newPassword, string oldPassword = "") {

            var tokenUser = await _authService.ExtractTokenInfo(token);

            if (tokenUser is null)
                return new ServiceResponse<string> { Success = false, Message = "Action failed: This token is not valid." };


            var userToChangePassword = await _repository.GetByIdAsync(userId);

            if (tokenUser.Role.ToString() != "Admin" && userToChangePassword.Id != tokenUser.Id)
                return new ServiceResponse<string> { Success = false, Message = "Action failed: You do not have the required permissions." };

            if (!VerifyPassword(oldPassword, userToChangePassword.PasswordSalt, userToChangePassword.PasswordHash) && tokenUser.Role.ToString() != "Admin")
                return new ServiceResponse<string> { Success = false, Message = "Action failed: Incorrect password." };

            var newHashedPassword = GenerateHash(newPassword);
            var info = newHashedPassword.Split(':');

            await _repository.UpdatePassword(userToChangePassword, info[0], info[1]);

            return new ServiceResponse<string> { Success = true, Message = "Password has been successfully updated." };
        }




        private static string GenerateHash (string password) {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[16];

            rng.GetBytes(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
        private static bool VerifyPassword (string insertedPassword, string storedSalt, string storedHash) {

            byte[] salt = Convert.FromBase64String(storedSalt);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: insertedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return hash == storedHash;
        }
    }
}
