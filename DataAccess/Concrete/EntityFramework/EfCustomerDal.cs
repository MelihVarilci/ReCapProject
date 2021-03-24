using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal:EfEntityRepositoryBase<Customer,ReCapContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from ct in filter == null ? context.Customers : context.Customers.Where(filter)
                    join us in context.Users 
                        on ct.UserId equals us.UserId
                    select new CustomerDetailDto
                    {
                        CustomerId = ct.CustomerId,
                        UserId = us.UserId,
                        CompanyName = ct.CompanyName,
                        Email = us.Email,
                        FirstName = us.FirstName,
                        LastName = us.LastName,
                        Status = us.Status
                    };
                return result.ToList();

            }
        }
    }
}
