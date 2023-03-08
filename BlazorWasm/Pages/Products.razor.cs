using BlazorWasm.HttpRepository;
using Microsoft.AspNetCore.Components;
using Northwind.Contracts.Models;

namespace BlazorWasm.Pages
{
    public partial class Products
    {
       
        public List<ProductDto> ProductList { get; set; } = new List<ProductDto>();

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ProductList = await ProductRepo.GetProducts();

            foreach (var item in ProductList)
            {
                Console.WriteLine(item.ProductName);
            }
        }
    }
}
