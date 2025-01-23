using api_painel_producao.DTOs;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;

namespace api_painel_producao.Services {

    public interface IOrderService { 
        Task<ServiceResponse<int>> CreateOrderAsync (string token, CreateOrderViewModel orderData);
        Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(int id);
    }

    public class OrderService : IOrderService {

        private readonly IOrderRepository _orderRepository;
        private readonly IFramedArtworkRepository _framedArtworkRepository;
        private readonly IAuthService _authService;

        public OrderService (IOrderRepository repository, IAuthService authService, IFramedArtworkRepository framedArtworkRepository) {
            _orderRepository = repository;
            _authService = authService;
            _framedArtworkRepository = framedArtworkRepository;
        }

        public async Task<ServiceResponse<int>> CreateOrderAsync (string token, CreateOrderViewModel orderData) { 

            var userData = await _authService.ExtractTokenInfo(token);

            var orderToAdd = OrderDTO.Create(orderData);

            await _orderRepository.CreateOrderAsync(orderToAdd, userData.Id);

            if (orderToAdd.Id is null)
                return ServiceResponse<int>.Fail("Action failed: internal error.");

            foreach (var item in orderToAdd.FramedArtworks) {
                await _framedArtworkRepository.AddFramedArtworkToOrder(item, orderToAdd.Id.Value);
            };

            return ServiceResponse<int>.Ok(orderToAdd.Id.Value);
        }

        public async Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync (int id) {
            try
            {
                var orderFound = await _orderRepository.GetOrderById(id);

                if (orderFound is null)
                    return ServiceResponse<OrderDTO>.Fail("Action failed: This order does not exist.");

                return ServiceResponse<OrderDTO>.Ok(orderFound);

            }
            catch (Exception e)
            {
                return ServiceResponse<OrderDTO>.Fail("Action failed: internal error.");
            }
        }
    }
}
