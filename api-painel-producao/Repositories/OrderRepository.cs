using api_painel_producao.Data;

namespace api_painel_producao.Repositories {

    public interface IOrderRepository {
        
    }

    public class OrderRepository : IOrderRepository {

        private readonly AppDbContext _context;

        public OrderRepository (AppDbContext context) {
            _context = context;
        }







    }
}
