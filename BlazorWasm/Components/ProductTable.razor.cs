using Microsoft.AspNetCore.Components;
using Northwind.Contracts.Models;

namespace BlazorWasm.Components
{
    public partial class ProductTable
    {
        [Parameter]
        public List<ProductDto> Products { get; set; }
    }
}
