using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Order.Application.Interfaces
{
    public class ProductDetailsDto
    {
        public Guid ProductId { get; set; } 
        public decimal Price { get; set; } 
        public bool IsPhysical { get; set; } 
        public int StockQuantity { get; set; }
    }

    public interface IProductService
    {
        Task<List<ProductDetailsDto>> GetProductDetails(List<Guid> productIds);
    }
}