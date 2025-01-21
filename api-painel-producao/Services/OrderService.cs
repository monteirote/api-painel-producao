using api_painel_producao.DTOs;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;

namespace api_painel_producao.Services {

    public interface IOrderService { 
        Task<ServiceResponse<int>> CreateOrderAsync (string token, CreateOrderViewModel orderData);
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

            OrderDTO orderToAdd = OrderDTO.Create(orderData);

            var orderCreated = await _orderRepository.CreateOrderAsync(orderToAdd, userData.Id);

            foreach (var item in orderToAdd.FramedArtworks) { 
             // adicionar os framed artworks
            };
        }
    }
}
