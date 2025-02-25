using api_painel_producao.Models;
using api_painel_producao.Models.DTOs;
using api_painel_producao.Models.RequestModels.Order;
using api_painel_producao.ViewModels;

namespace api_painel_producao.DTOs
{

    public class OrderDTO {
        public int? Id { get; set; }

        public string Reference { get; set; }
        public string Priority { get; set; }
        public decimal TotalPrice { get; set; }

        public List<FramedArtworkDTO> FramedArtworks { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime CreatedAt { get; set; }
        public UserDTO CreatedBy { get; set; }
        public CustomerDTO CreatedFor { get; set; }
        public int CreatedForId { get; set; }

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
                CreatedForId = orderData.CustomerId
            };
        }

        public static OrderDTO Create (Order orderData) {
            if (orderData is null) return null;

            return new OrderDTO {
                Id = orderData.Id,
                Reference = orderData.Reference,
                Priority = orderData.Priority.ToString(),
                TotalPrice = orderData.TotalPrice,

                FramedArtworks = (from fa in orderData.FramedArtworks
                                  select new FramedArtworkDTO(fa)).ToList(),

                IsCanceled = orderData.IsCanceled,

                CreatedAt = orderData.CreatedAt,
                CreatedBy = new UserDTO(orderData.CreatedBy),
                CreatedFor = new CustomerDTO(orderData.CreatedFor)
            };
        }
    }

    public class SimplifiedOrderDTO {
        public int Id { get; set; }

        public string Reference { get; set; }
        public string Priority { get; set; }
        public decimal TotalPrice { get; set; }

        public List<FramedArtworkDTO> FramedArtworks { get; set; }

        public SimplifiedUserDTO CreatedBy { get; set; }
        public SimplifiedCustomerDTO CreatedFor { get; set; }

        public SimplifiedOrderDTO (Order orderData) {
            this.Id = orderData.Id;

            this.Reference = orderData.Reference;
            this.Priority = orderData.Priority.ToString();
            this.TotalPrice = orderData.TotalPrice;

            this.FramedArtworks = (from fa in orderData.FramedArtworks
                                   select new FramedArtworkDTO(fa)).ToList();

            this.CreatedBy = new SimplifiedUserDTO (orderData.CreatedBy);
            this.CreatedFor = new SimplifiedCustomerDTO (orderData.CreatedFor);
        }
    }
}
