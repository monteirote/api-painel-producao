using api_painel_producao.DTOs;
using api_painel_producao.Models;
using api_painel_producao.Models.RequestModels.Order;
using api_painel_producao.Models.ResponseModels.Order;
using api_painel_producao.Repositories;
using api_painel_producao.Utils;
using api_painel_producao.ViewModels;

namespace api_painel_producao.Services {

    public interface IOrderService { 
        Task<ServiceResponse<int>> CreateOrderAsync (string token, OrderDataRequestModel orderData);
        Task<ServiceResponse<DetailedOrderResponseModel>> GetOrderByIdAsync (int id);
        Task<ServiceResponse<List<OrderResponseModel>>> GetAllOrders ();
        Task<ServiceResponse<int>> CancelOrderById (int id, string token);
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

        public async Task<ServiceResponse<int>> CreateOrderAsync (string token, OrderDataRequestModel orderData) { 

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

        public async Task<ServiceResponse<DetailedOrderResponseModel>> GetOrderByIdAsync (int id) {
            try
            {
                var orderFound = await _orderRepository.GetOrderById(id);

                if (orderFound is null)
                    return ServiceResponse<DetailedOrderResponseModel>.Fail("Action failed: This order does not exist.");

                var result = DetailedOrderResponseModel.Create(orderFound);

                return ServiceResponse<DetailedOrderResponseModel>.Ok(result);

            }
            catch (Exception e)
            {
                return ServiceResponse<DetailedOrderResponseModel>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<List<OrderResponseModel>>> GetAllOrders () {
            try { 

                var ordersFound = await _orderRepository.GetAllOrdersAsync();

                var results = ordersFound.Select(x => OrderResponseModel.Create(x)).ToList();

                return ServiceResponse<List<OrderResponseModel>>.Ok(results);

            } catch (Exception e) {
                return ServiceResponse<List<OrderResponseModel>>.Fail("Action failed: internal error.");
            }
        }

        public async Task<ServiceResponse<int>> CancelOrderById (int id, string token) {
            try {
                var orderFound = await _orderRepository.GetOrderById(id);

                var userInfo = await _authService.ExtractTokenInfo(token);

                if (orderFound is null)
                    return ServiceResponse<int>.Fail("Action failed: This customer does not exist.");

                if (!orderFound.IsCanceled)
                    return ServiceResponse<int>.Fail("Action failed: This order is already canceled.");


                await _orderRepository.CancelOrderAsync(id, userInfo.Id);

                return ServiceResponse<int>.Ok();

            }
            catch
            {
                return ServiceResponse<int>.Fail("Action failed: internal error.");
            }
        }
    }
}
