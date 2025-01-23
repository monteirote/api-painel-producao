using api_painel_producao.Data;
using api_painel_producao.DTOs;
using api_painel_producao.Models;
using api_painel_producao.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace api_painel_producao.Repositories {

    public interface IOrderRepository {
        Task CreateOrderAsync (OrderDTO orderData, int userId);
        Task<OrderDTO> GetOrderById (int id);
    }

    public class OrderRepository : IOrderRepository {

        private readonly AppDbContext _context;

        public OrderRepository (AppDbContext context) {
            _context = context;
        }

        public async Task CreateOrderAsync (OrderDTO orderData, int userId) {

            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var createdFor = await _context.Customers.FirstOrDefaultAsync(x => x.Id == orderData.CreatedForId);

            if (createdBy is null || createdFor is null)
                return;

            var newOrder = new Order {
                CreatedForId = orderData.CreatedForId,
                CreatedById = createdBy.Id,
                Reference = orderData.Reference,
                Priority = (Priority) Enum.Parse(typeof(Priority), orderData.Priority),
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            orderData.Id = newOrder.Id;

            return;
        }

        public async Task<OrderDTO> GetOrderById (int id) {

            var order = await _context.Orders
                .Include(x => x.CreatedBy)
                .Include(x => x.CreatedFor)
                .Include(x => x.FramedArtworks)
                    .ThenInclude(f => f.Glass)
                .Include(x => x.FramedArtworks)
                    .ThenInclude(f => f.Frame)
                .Include(x => x.FramedArtworks)
                    .ThenInclude(f => f.Paper)
                .Include(x => x.FramedArtworks)
                    .ThenInclude(f => f.Background)
                .FirstOrDefaultAsync(x => x.Id == id);

            return OrderDTO.Create(order);
        }





    }
}
