using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WPF.App.Service.Rental;
using WPF.App.ViewModel.DomainClass;
using WPF.App.ViewModel.Movie;

namespace WPF.App.Service.Customer;

public class RentalService : IRentalService
{
    private readonly HttpClient _httpClient;

    public RentalService() {

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiRental"]);

    }

    public DomainClass<IEnumerable<Models.Rental>> GetByNameCustom(string name)
    {
        var response = _httpClient.GetAsync($"Rental").Result;
        response.EnsureSuccessStatusCode();

        var json = response.Content.ReadAsStringAsync().Result;
        var data = JsonConvert.DeserializeObject<DomainClass<IEnumerable<Models.Rental>>>(json);
        return data;
    }

    public DomainClass<Models.Rental> Save(RentalViewModelRequest vmRental)
    {
        string json = JsonConvert.SerializeObject(vmRental);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = _httpClient.PostAsync("Rental", content).Result;            
       
        string responseJson =  response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<DomainClass<Models.Rental>>(responseJson);
    }
}
