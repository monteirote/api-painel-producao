using api_painel_producao.DTOs;
using api_painel_producao.Models.Enums;
using api_painel_producao.Models.ResponseModels.Customer;

namespace api_painel_producao.Models.ResponseModels.Order {
    public class OrderResponseModel {
        public int Id { get; set; }

        public string Reference { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public string Priority { get; set; } = string.Empty;

        public DateTime ExpectedDeliveryDate { get; set; }

        public CustomerResponseModel? Customer { get; set; }

        public static OrderResponseModel Create (OrderDTO orderData) {
            if (orderData is null) return null;

            return new OrderResponseModel { 
                Id = orderData.Id ?? 0,
                Reference = orderData.Reference,
                TotalPrice = orderData.TotalPrice,
                Priority = orderData.Priority,
                ExpectedDeliveryDate = orderData.ExpectedDeliveryDate,
                Customer = CustomerResponseModel.Create(orderData.CreatedFor)
            };
        }

    }
}
