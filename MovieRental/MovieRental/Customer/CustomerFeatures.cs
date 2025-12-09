using System.Net;
using Microsoft.EntityFrameworkCore;
using MovieRental.Controllers.DomainClass;
using MovieRental.Data;

namespace MovieRental.Customer;

public class CustomerFeatures : ICustomerFeatures
{
    private readonly MovieRentalDbContext _movieRentalDb;

    
    public CustomerFeatures(MovieRentalDbContext movieRentalDb)
    {
        _movieRentalDb = movieRentalDb;
    }

    public DomainClass<List<Customer>> GetByCustomerName(string customerName)
    {
        List<Customer> data = null;
        if (string.IsNullOrEmpty(customerName))
        {
            data = _movieRentalDb.Customers.ToList();
        }
        else
            data = _movieRentalDb.Customers.Where(x => x.Name.Contains(customerName)).ToList();

        return new DomainClass<List<Customer>>(data);
    }

    public async Task<DomainClass<Customer>> Save(Customer customer)
    {
        var classDomain = new DomainClass<Customer>(customer);

        #region Validação
        
        if (string.IsNullOrEmpty(customer.Name))
            classDomain.MsgsErro.Add("Name", "The value cannot be empty.");

        if (classDomain.Success is false)
        {
            classDomain.AddStatusCode(HttpStatusCode.BadRequest);
            return classDomain;
        }
			
        if (await _movieRentalDb.Customers.AnyAsync(x => x.Name.Equals(customer.Name)))
            classDomain.MsgsErro.Add("Name", "The value shown is already in the database.");
			
        if (classDomain.Success is false)
        {
            classDomain.AddStatusCode(HttpStatusCode.Conflict);
            return classDomain;
        }


        #endregion

        #region Persistencia

        _movieRentalDb.Customers.Add(customer);
        var dbStatus = await _movieRentalDb.SaveChangesAsync();

        if (dbStatus <= 0)
        {
            classDomain.MsgsErro.Add("Database", "There was an error in this operation.");
            classDomain.AddStatusCode(HttpStatusCode.InternalServerError);
        }

        return classDomain;

        #endregion
    }
}