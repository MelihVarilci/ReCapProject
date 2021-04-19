using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<Customer>> GetByCustomerId(int id);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IDataResult<CustomerDetailDto> GetByCustomerEmail(string email);
        IDataResult<CustomerDetailDto> GetCustomerDetailById(int customerId);
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
    }
}
