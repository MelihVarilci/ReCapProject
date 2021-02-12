using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetByDailyPrice(decimal min, decimal max);
        List<Car> GetCarsByBrandId(int id);
        List<Car> GetCarsByColorId(int id);
        List<Car> GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
