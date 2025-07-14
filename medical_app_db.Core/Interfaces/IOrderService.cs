using medical_app_db.Core.DTOs.Order;

namespace medical_app_db.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IReadOnlyList<OrderToReturnDTO>> GetOrdersAsync(int pageSize = 5, int pageIndex = 1);
        Task<OrderToReturnDTO?> GetOrderByIdAsync(Guid id);
        Task<OrderServiceResult?> CreateOrderAsync(OrderDTO orderDto);
        Task<IReadOnlyList<OrderToReturnDTO>?> GetNotDeliveredOrders(int pageSize = 5, int pageIndex = 1);
        Task<IReadOnlyList<OrderToReturnDTO>?> GetUserOrders(int pageSize = 5, int pageIndex = 1);
        Task<IReadOnlyList<OrderToReturnDTO>?> GetBranchOrdersAsync(Guid branchId, int pageSize = 5, int pageIndex = 1);
        Task<OrderServiceResult?> UpdateOrderAsync(Guid id, OrderDTO orderDto);
        Task<OrderServiceResult?> DeleteOrderAsync(Guid id);
        Task<OrderServiceResult?> MarkAsPaid(Guid id);
    }
}
