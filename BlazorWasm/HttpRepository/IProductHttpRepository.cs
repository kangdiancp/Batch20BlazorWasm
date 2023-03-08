using Northwind.Contracts.Models;


namespace BlazorWasm.HttpRepository
{
    public interface IProductHttpRepository
    {
        Task<List<ProductDto>> GetProducts();
    }
}
