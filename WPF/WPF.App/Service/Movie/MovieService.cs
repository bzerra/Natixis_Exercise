using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WPF.App.Models;
using WPF.App.ViewModel.Customer;
using WPF.App.ViewModel.DomainClass;
using WPF.App.ViewModel.Movie;

namespace WPF.App.Service.Customer;

public class MovieService : IMovieService
{
    private readonly HttpClient _httpClient;

    public MovieService() {

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiRental"]);

    }

    public DomainClass<IEnumerable<Models.Movie>> GetAll()
    {
        var response = _httpClient.GetAsync($"Movie").Result;
        response.EnsureSuccessStatusCode();

        var json = response.Content.ReadAsStringAsync().Result;
        var data = JsonConvert.DeserializeObject<DomainClass<IEnumerable<Models.Movie>>>(json);
        return data;
    }

    public DomainClass<Models.Movie> Save(MovieViewModelRequest vmCustomer)
    {
        string json = JsonConvert.SerializeObject(vmCustomer);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = _httpClient.PostAsync("Movie", content).Result;            
       
        string responseJson =  response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<DomainClass<Models.Movie>>(responseJson);
    }
}
