using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Repositories;
using api_painel_producao.Models;
using api_painel_producao.Models.RequestModels.Login;
using api_painel_producao.Models.ResponseModels.Login;

namespace api_painel_producao.Services {

    public interface IAccountService {
        Task<ServiceResponse<int>> CreateUserAsync (SignupRequestModel userData);
        Task<ServiceResponse<string>> LoginAsync (LoginRequestModel user);
        Task<ServiceResponse<string>> DeactivateUserAsync (string token, int userId);
        Task<ServiceResponse<string>> ChangePasswordAsync (string token, int userId, string newPassword, string oldPassword = "");
        Task<ServiceResponse<string>> ActivateUserAsync (string token, int userId);
        Task<ServiceResponse<List<UserPendingApprovalResponseModel>>> RetrieveUsersPendingApproval ();
    }


    public class AccountService : IAccountService {

        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public AccountService (IUserRepository repository, IAuthService authService) {
            _repository = repository;
            _authService = authService;
        }

        public async Task<ServiceResponse<int>> CreateUserAsync (SignupRequestModel userData) {
            try {

                if (await _repository.FindUserByEmailAsync(userData.Email) != null)
                    return ServiceResponse<int>.Fail("Action failed: An account with this email already exists.");

                if (await _repository.FindUserByUsernameAsync(userData.Username) != null)
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

        public async Task<ServiceResponse<string>> LoginAsync (LoginRequestModel userData) {
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

            var userToDeactivate = await _repository.FindUserByIdAsync(userId);

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

            var userToChangePassword = await _repository.FindUserByIdAsync(userId);

            if (userToChangePassword is null)
                return ServiceResponse<string>.Fail("Action failed: This user does not exist.");


            if (tokenUser.Role.ToString() != "Admin" && userToChangePassword.Id != tokenUser.Id)
                return ServiceResponse<string>.DenyPermission();

            if (!VerifyPassword(oldPassword, userToChangePassword.PasswordSalt, userToChangePassword.PasswordHash) && tokenUser.Role.ToString() != "Admin")
                return ServiceResponse<string>.Fail("Action failed: Incorrect password.");

            var newHashedPassword = GenerateHash(newPassword);
            string[] passwordData = newHashedPassword.Split(':');

            await _repository.UpdatePassword(userId, passwordData, tokenUser.Id);

            return ServiceResponse<string>.Ok("Password has been successfully updated.");
        }

        public async Task<ServiceResponse<string>> ActivateUserAsync (string token, int userId) {

            var userToActivate = await _repository.FindUserByIdAsync(userId);

            var tokenInfo = await _authService.ExtractTokenInfo(token);

            if (userToActivate is null)
                return ServiceResponse<string>.Fail("Action failed: User not found.");

            if (userToActivate.IsActive)
                return ServiceResponse<string>.Fail("Action failed: User is already active.");

            await _repository.ActivateUserAsync(userId, tokenInfo.Id);

            return ServiceResponse<string>.Ok("User has been successfully activated.");
        }

        public async Task<ServiceResponse<List<UserPendingApprovalResponseModel>>> RetrieveUsersPendingApproval () {

            var usersRetrieved = await _repository.RetrieveUsersPendingApproval();

            var results = usersRetrieved.Select(x => UserPendingApprovalResponseModel.Create(x)).ToList();

            return ServiceResponse<List<UserPendingApprovalResponseModel>>.Ok(results);
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
