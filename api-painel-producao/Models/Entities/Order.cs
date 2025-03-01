using api_painel_producao.Models.Enums;
using api_painel_producao.Models;

namespace api_painel_producao.Models {
    public class Order {

        public int Id { get; set; }

        public string Reference { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public Priority Priority { get; set; }

        public List<FramedArtwork> FramedArtworks { get; set; } = [];


        public DateTime CreatedAt { get; set; }
        public Customer? CreatedFor { get; set; }
        public int? CreatedForId { get; set; }


        public User CreatedBy { get; set; }
        public int? CreatedById { get; set; }


        public DateTime? LastModifiedAt { get; set; }
        public User? LastModifiedBy { get; set; }
        public int? LastModifiedById { get; set; }

        public DateTime ExpectedDeliveryDate {  get; set; }

        public bool IsCanceled { get; set; } = false;
        public DateTime? CanceledAt { get; set; }
        public User? CanceledBy { get; set; }
        public int? CanceledById { get; set; }

    }
}
