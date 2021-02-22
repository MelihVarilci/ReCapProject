using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,ReCapContext>,IRentalDal
    {
        
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from rt in context.Rentals
                    join cr in context.Cars on rt.CarId equals cr.CarId
                    join cst in context.Customers on rt.CustomerId equals cst.CustomerId
                    join usr in context.Users on cst.UserId equals usr.UserId
                    join brd in context.Brands on cr.BrandId equals brd.BrandId
                    join clr in context.Colors on cr.ColorId equals clr.ColorId 
                    select new RentalDetailDto
                    {
                        CompanyName = cst.CompanyName,
                        CarModelYear = cr.CarModelYear,
                        CarDailyPrice = cr.CarDailyPrice,
                        CarDescription = cr.CarDescription,
                        CarId = rt.CarId,
                        FirstName = usr.FirstName,
                        LastName = usr.LastName,
                        BrandName = brd.BrandName,
                        ColorName = clr.ColorName,
                        RentDate = rt.RentDate,
                        ReturnDate = rt.ReturnDate
                    };
                return result.ToList();
            }
        }
        
    }
}
