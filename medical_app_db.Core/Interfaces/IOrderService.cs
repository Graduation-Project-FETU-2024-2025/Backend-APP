using medical_app_db.Core.DTOs.Order;

namespace medical_app_db.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IReadOnlyList<OrderToReturnDTO>> GetOrdersAsync(int pageSize = 5, int pageIndex = 1);
        Task<OrderToReturnDTO?> GetOrderByIdAsync(Guid id);
        Task<OrderServiceResult?> CreateOrderAsync(OrderDTO orderDto);
    }
}
