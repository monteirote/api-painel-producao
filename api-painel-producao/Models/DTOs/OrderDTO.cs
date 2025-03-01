using api_painel_producao.Models;
using api_painel_producao.Models.DTOs;
using api_painel_producao.Models.RequestModels.Order;

namespace api_painel_producao.DTOs {

    public class OrderDTO {
        public int? Id { get; set; }

        public string Reference { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public List<FramedArtworkDTO> FramedArtworks { get; set; } = [];

        public bool IsCanceled { get; set; }

        public DateTime CreatedAt { get; set; }
        public UserDTO CreatedBy { get; set; } = new UserDTO();
        public CustomerDTO CreatedFor { get; set; } = new CustomerDTO();
        public int CreatedForId { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public UserDTO? LastModifiedBy { get; set; }


        public DateTime? CanceledAt { get; set; }
        public UserDTO? CanceledBy { get; set; }
       

        public static OrderDTO Create (OrderDataRequestModel orderData) {
            if (orderData is null) return null;

            return new OrderDTO { 
                Reference = orderData.Reference,
                Priority = orderData.Priority,
                TotalPrice = orderData.TotalPrice,
                FramedArtworks = orderData.FramedArtworks.Select(x => FramedArtworkDTO.Create(x)).ToList(),
                CreatedForId = orderData.CustomerId,
                ExpectedDeliveryDate = orderData.ExpectedDeliveryDate,
            };
        }

        public static OrderDTO Create (Order orderData) {
            if (orderData is null) return null;

            return new OrderDTO {
                Id = orderData.Id,
                Reference = orderData.Reference,
                Priority = orderData.Priority.ToString(),
                TotalPrice = orderData.TotalPrice,

                FramedArtworks = orderData.FramedArtworks.Select(x => FramedArtworkDTO.Create(x)).ToList(),

                IsCanceled = orderData.IsCanceled,

                CreatedAt = orderData.CreatedAt,
                CreatedBy = UserDTO.Create(orderData.CreatedBy),
                CreatedFor = CustomerDTO.Create(orderData.CreatedFor)
            };
        }
    }
}
