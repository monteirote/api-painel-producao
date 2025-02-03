using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_painel_producao.Models {
    public class Customer {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public User? DeactivatedBy { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public User? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public List<Order> Orders { get; set; }


        [ForeignKey("CreatedBy")]
        public int? CreatedById { get; set; }

        [ForeignKey("LastModifiedBy")]
        public int? LastModifiedById { get; set; }

        [ForeignKey("DeactivatedBy")]
        public int? DeactivatedById { get; set; }

    }
}
