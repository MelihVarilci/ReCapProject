using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId);
        IDataResult<CarDetailDto> GetCarDetailsByCarId(int carId);
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int carId);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarDetailsFilter(int brandId, int colorId);
        IResult AddTransactionalTest(Car car);
    }
}