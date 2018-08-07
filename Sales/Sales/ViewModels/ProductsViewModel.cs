namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Common.Models;
    using Services;

    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async Task LoadProducts()
        {
            var response = await apiService.GetList<Product>("https://salesapiservices.azurewebsites.net", "/api", "/Products");
            if (response.IsSuccess)
            {
                var products = (List<Product>)response.Result;
                this.Products = new ObservableCollection<Product>(products);
            }
        }
    }
}