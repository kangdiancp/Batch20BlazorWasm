using BlazorWasm.Client.Features;
using Microsoft.AspNetCore.WebUtilities;
using Northwind.Contracts.Models;
using Northwind.Domain.Entities;
using Northwind.Domain.RequestFeatures;
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

        public async Task<PagingResponse<ProductDto>> GetProductPaging(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("product/pageList",queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ProductDto>
            {
                Items = JsonSerializer.Deserialize<List<ProductDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;


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
