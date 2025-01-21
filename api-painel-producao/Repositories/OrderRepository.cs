using api_painel_producao.Data;
using api_painel_producao.DTOs;
using api_painel_producao.Models;
using api_painel_producao.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface IOrderRepository {
        Task<bool> CreateOrderAsync (OrderDTO orderData, int userId);
    }

    public class OrderRepository : IOrderRepository {

        private readonly AppDbContext _context;

        public OrderRepository (AppDbContext context) {
            _context = context;
        }

        public async Task<OrderDTO> CreateOrderAsync (OrderDTO orderData, int userId) {

            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (createdBy is null) return null;

            var createdFor = await _context.Customers.FirstOrDefaultAsync(x => x.Id == orderData.CreatedForId);

            var newOrder = new Order {
                CreatedFor = createdFor,
                CreatedBy = createdBy,
                Reference = orderData.Reference,
                Priority = (Priority) Enum.Parse(typeof(Priority), orderData.Priority),
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return OrderDTO.Create(newOrder);
        }





    }
}
