using AutoMapper;
using Library.Order.Application.Commands;
using Library.Order.Application.Queries;
using Library.Order.Domain.Entities;

namespace Library.Order.Application.Mappers
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Domain.Entities.Order, OrderResponse>(); 
            CreateMap<Domain.Entities.Order, OrderResponseDto>(); 
            CreateMap<OrderItem, OrderItemResponseDto>();
        }
    }
}