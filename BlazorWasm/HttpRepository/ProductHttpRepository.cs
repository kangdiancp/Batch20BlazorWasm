using Northwind.Contracts.Models;
using System.Text.Json;

namespace BlazorWasm.HttpRepository
{
    public class ProductHttpRepository : IProductHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProductHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            //call api end point, e.g 
            var response = await _httpClient.GetAsync("product");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var products = JsonSerializer.Deserialize<List<ProductDto>>(content,_options);
            return products;
        }
    }
}
