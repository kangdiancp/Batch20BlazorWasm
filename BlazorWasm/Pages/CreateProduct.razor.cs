

using BlazorWasm.HttpRepository;
using BlazorWasm.Shared;
using Microsoft.AspNetCore.Components;
using Northwind.Contracts.Models;
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Pages
{
    public partial class CreateProduct
    {
        private ProductCreateDto _product = new ProductCreateDto();
        private List<SupplierDto> suppliers = new List<SupplierDto>();

		private SuccessNotification _notification;

		[Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        private async Task Create()
        {
            await ProductRepo.CreateProduct(_product);
			_notification.Show("/productPaging");
		}

        protected async override Task OnInitializedAsync()
        {
            // call method getSupplier to fill suppliers variable, execute before page createProduct rendered
            suppliers = await ProductRepo.GetSupplier();

        }
    }
}
