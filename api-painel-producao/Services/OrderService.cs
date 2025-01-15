using api_painel_producao.Repositories;

namespace api_painel_producao.Services {

    public interface IOrderService { 
    
    }

    public class OrderService : IOrderService {

        private readonly IOrderRepository _orderRepository;

        public OrderService (IOrderRepository repository) {
            _orderRepository = repository;
        }
    }
}
