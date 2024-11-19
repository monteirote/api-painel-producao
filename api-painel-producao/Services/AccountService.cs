using api_painel_producao.ViewModels;
using api_painel_producao.Utils;
using api_painel_producao.Repositories;

namespace api_painel_producao.Services {

    public interface IAccountService {
        Task<ServiceResponse<string>> CreateUserAsync (UserSignupViewModel user);
        //Task UpdateAsync (User user);
        //Task DeleteAsync (User user);
        //Task<List<User>> GetAllAsync ();
        //Task<User?> GetByIdAsync (int id);
    }


    public class AccountService : IAccountService {

        private readonly IUserRepository _repository;

        public AccountService (IUserRepository repository) {
            _repository = repository;
        }

        public async Task<ServiceResponse<string>> CreateUserAsync (UserSignupViewModel user) {
            return null;
        }
    }
}
