using api_painel_producao.DTOs;
using api_painel_producao.Models.Enums;
using api_painel_producao.Models.ResponseModels.Customer;
using api_painel_producao.Models.ResponseModels.FramedArtwork;
using api_painel_producao.Models.ResponseModels.Login;

namespace api_painel_producao.Models.ResponseModels.Order {
    public class DetailedOrderResponseModel {

        public int Id { get; set; }

        public string Reference { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public string Priority { get; set; } = string.Empty;

        public List<FramedArtworkResponseModel> FramedArtworks { get; set; } = [];

        public DateTime CreatedAt { get; set; }

        public CustomerResponseModel? Customer { get; set; }

        public UserResponseModel? CreatedBy { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        public UserResponseModel? LastModifiedBy { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime? CanceledAt { get; set; }

        public UserResponseModel? CanceledBy { get; set; }


        public static DetailedOrderResponseModel Create (OrderDTO orderData) {
            if (orderData is null) return null;

            return new DetailedOrderResponseModel { 
                Id = orderData.Id ?? 0,
                Reference = orderData.Reference,
                TotalPrice = orderData.TotalPrice,
                Priority = orderData.Priority,
                FramedArtworks = orderData.FramedArtworks.Select(x => FramedArtworkResponseModel.Create(x)).ToList(),
                CreatedAt = orderData.CreatedAt,
                Customer = CustomerResponseModel.Create(orderData.CreatedFor),
                CreatedBy = UserResponseModel.Create(orderData.CreatedBy),
                ExpectedDeliveryDate = orderData.ExpectedDeliveryDate,
                LastModifiedAt = orderData.LastModifiedAt,  
                LastModifiedBy = UserResponseModel.Create(orderData.LastModifiedBy),
                IsCanceled = orderData.IsCanceled,
                CanceledAt = orderData.CanceledAt,
                CanceledBy = UserResponseModel.Create(orderData.CanceledBy)
            };
        }

    }
}
