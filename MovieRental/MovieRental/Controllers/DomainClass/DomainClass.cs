using System.Net;

namespace MovieRental.Controllers.DomainClass;

public class DomainClass<T>
{
    public bool Success { get => MsgsErro.Any() ? false : true; }
    public Dictionary<string, string> MsgsErro { get; set; }
    public T Entity { get; set; }
    public HttpStatusCode Status { get; set; }

    public DomainClass(T entity)
    {
        Status = HttpStatusCode.OK;
        MsgsErro = new Dictionary<string, string>();
        Entity = entity;
    }
    
    public DomainClass()
    {
        MsgsErro = new Dictionary<string, string>();
    }

    public void AddClass(T classDomain)
    {
        Entity = classDomain;
    }

    public void AddStatusCode(HttpStatusCode code)
    {
        Status = code;
    } 
}