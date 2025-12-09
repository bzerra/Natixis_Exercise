using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.DomainClass;

namespace WPF.App.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService() {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiRental"]);

        }

        public DomainClass<IEnumerable<Models.Customer>> GetByName(string nameCustomer)
        {
            var filter = string.IsNullOrEmpty(nameCustomer) ? "" : $"?customerName={nameCustomer}";

            var response = _httpClient.GetAsync($"Customer{filter}").Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<DomainClass<IEnumerable<Models.Customer>>>(json);
            return data;
        }

        public DomainClass<Models.Customer> Save(CustomerViewModelRequest vmCustomer)
        {
            string json = JsonConvert.SerializeObject(vmCustomer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = _httpClient.PostAsync("Customer", content).Result;            
           
            string responseJson =  response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<DomainClass<Models.Customer>>(responseJson);
        }
    }
}
