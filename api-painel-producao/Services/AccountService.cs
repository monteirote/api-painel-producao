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
        Task<ServiceResponse<string>> ChangePasswordAsync (string token, int userId, string newPassword, string oldPassword = "");
        Task<ServiceResponse<string>> ActivateUserAsync (string token, int userId);
        Task<ServiceResponse<List<PendingApprovalUserViewModel>>> RetrieveUsersPendingApproval ();
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

                if (_repository.FindUserByEmailAsync(userData.Email) != null)
                    return ServiceResponse<int>.Fail("Action failed: An account with this email already exists.");

                if (_repository.FindUserByUsernameAsync(userData.Username) != null)
                    return ServiceResponse<int>.Fail("Action failed: An account with this username already exists.");

                var parts = GenerateHash(userData.Password).Split(':');

                var userToAdd = new User {
                    Name = userData.Name,
                    Email = userData.Email,
                    Username = userData.Username,
                    Role = Models.Enums.UserRole.Vendedor,
                    PasswordSalt = parts[0],
                    PasswordHash = parts[1],
                    CreatedAt = DateTime.Now,
                    IsActive = false
                };

                await _repository.CreateAsync(userToAdd);

                return ServiceResponse<int>.Ok(userToAdd.Id, "User created sucessfully!");

            } catch (Exception e) {
                return ServiceResponse<int>.Fail("Internal error creating user.");
            }
        }

        public async Task<ServiceResponse<string>> LoginAsync (UserLoginViewModel userData) {
            try {
                var foundUser = await _repository.FindUserByUsernameAsync(userData.Username);

                string errorMessage = "";

                if (foundUser is null || !VerifyPassword(userData.Password, foundUser.PasswordSalt, foundUser.PasswordHash))
                    return ServiceResponse<string>.Fail("Login failed: incorrect username or password.");

                if (!foundUser.IsActive)
                    return ServiceResponse<string>.Fail("Login failed: your account has been deactivated.");
                    

                var token = _authService.CreateToken(foundUser);

                return ServiceResponse<string>.Ok(token, "Login successful.");

            } catch (Exception e) {
                return ServiceResponse<string>.Fail("Login failed: internal error.");
            }
        }

        public async Task<ServiceResponse<string>> DeactivateUserAsync (string token, int userId) {

            var tokenUser = await _authService.ExtractTokenInfo(token);

            if (tokenUser is null)
                return ServiceResponse<string>.Fail("Action failed: This token is not valid.");

            var userToDeactivate = await _repository.GetByIdAsync(userId);

            if (userToDeactivate is null)
                return ServiceResponse<string>.Fail("Action failed: This user does not exist.");

            if (tokenUser.Role.ToString() != "Admin" && tokenUser.Id != userId)
                return ServiceResponse<string>.DenyPermission();

            await _repository.DeactivateUserAsync(userToDeactivate.Id, tokenUser.Id);

            return ServiceResponse<string>.Ok(null, "User has been successfully deactivated.");
        }

        public async Task<ServiceResponse<string>> ChangePasswordAsync (string token, int userId, string newPassword, string oldPassword = "") {

            var tokenUser = await _authService.ExtractTokenInfo(token);

            if (tokenUser is null)
                return ServiceResponse<string>.Fail("Action failed: This token is not valid.");


            var userToChangePassword = await _repository.GetByIdAsync(userId);

            if (tokenUser.Role.ToString() != "Admin" && userToChangePassword.Id != tokenUser.Id)
                return ServiceResponse<string>.DenyPermission();

            if (!VerifyPassword(oldPassword, userToChangePassword.PasswordSalt, userToChangePassword.PasswordHash) && tokenUser.Role.ToString() != "Admin")
                return ServiceResponse<string>.Fail("Action failed: Incorrect password.");

            var newHashedPassword = GenerateHash(newPassword);
            string[] passwordData = newHashedPassword.Split(':');

            await _repository.UpdatePassword(userId, passwordData, tokenUser.Id);

            return ServiceResponse<string>.Ok(null, "Password has been successfully updated.");
        }

        public async Task<ServiceResponse<string>> ActivateUserAsync (string token, int userId) {
            var userToActivate = await _repository.GetByIdAsync(userId);

            var tokenInfo = await _authService.ExtractTokenInfo(token);

            if (userToActivate is null)
                return ServiceResponse<string>.Fail("Action failed: User not found.");

            await _repository.ActivateUserAsync(userId, tokenInfo.Id);

            return ServiceResponse<string>.Ok("User has been successfully activated.");
        }

        public async Task<ServiceResponse<List<PendingApprovalUserViewModel>>> RetrieveUsersPendingApproval () {

            var usersRetrieved = await _repository.RetrieveUsersPendingApproval();

            var results = usersRetrieved.Select(x => {
                return new PendingApprovalUserViewModel {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Username = x.Username,
                    CreatedAt = x.CreatedAt
                };
            }).ToList();

            return ServiceResponse<List<PendingApprovalUserViewModel>>.Ok(results);
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
