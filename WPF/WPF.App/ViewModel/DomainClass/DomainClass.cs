using System.Net;

namespace WPF.App.ViewModel.DomainClass
{
    public class DomainClass<T>
    {
        public bool Success { get; set; }
        public Dictionary<string, string> MsgsErro { get; set; }
        public T Entity { get; set; }
        public HttpStatusCode Status { get; set; }
        
    }
}