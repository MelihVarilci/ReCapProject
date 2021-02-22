using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetByRentalId(int id);
        IDataResult<List<Rental>> GetByCarId(int id);
        IDataResult<List<Rental>> GetByCustomerId(int id);
        IDataResult<List<Rental>> GetByRentDate(DateTime first, DateTime last);
        IDataResult<Rental> CheckReturnDate(int carId);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
