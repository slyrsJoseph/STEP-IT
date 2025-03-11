using GameStoreProject.Services.Interfaces;
using GameStoreProject.Data.Models;
namespace GameStoreProject.Services.Classes;
using AutoMapper;
using GameStoreProject.DTO.Responses;
using GameStoreProject.Data.Repository;
using GameStoreProject.Services.Interfaces;
using GameStoreProject.Services.Classes;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task CreateOrder(CreateOrderRequest request)
    {
        _logger.LogInformation("Creating a new order: {@Request}", request);
        Client client = await _unitOfWork.ClientRep.GetClientByEmail(request.clientEmail);
        
        Order order = _mapper.Map<Order>(request);
        await _unitOfWork.OrderRep.AddAsync(order); 
        await _unitOfWork.SaveChangesAsync();
        
        await _unitOfWork.OrderRep.AddOrderToClient(client, order);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task ChangeDataOrder(ChangeOrderDataRequest request)
    {
        _logger.LogInformation("Changing order: {@Request}", request);
        Order order = await _unitOfWork.OrderRep.GetById(request.orderId);
        
        order = _mapper.Map(request, order);
        _unitOfWork.OrderRep.Update(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteOrder(DeleteOrderRequest request)
    {
        _logger.LogInformation("Deleting Order: {@Request}", request);
        Order order = await _unitOfWork.OrderRep.GetById(request.orderId);
        
        if (order == null)
        {
            throw new KeyNotFoundException($"Client with id {request.orderId} not found");
        }
        
        _unitOfWork.OrderRep.Delete(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<OrderResponse> FindOrder(FindOrderRequest request)
    {
        _logger.LogInformation("Client Search: {@Request}", request);
        Order order = await _unitOfWork.OrderRep.GetOrderInclude(request.orderId);
        return _mapper.Map<OrderResponse>(order);
    }
    
}