using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICarDal
    {
        // Kullanmak istediğimiz methodları oluşturuyoruz.
        List<Car> GetById(int carId);
        List<Car> GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);

    }
}
