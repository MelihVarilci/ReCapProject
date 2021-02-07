using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager:ICarService
    {
        ICarDal _cars;

        public CarManager(ICarDal cars)
        {
            _cars = cars;
        }

        public List<Car> GetById(int carId)
        {
            return _cars.GetById(carId);
        }

        public List<Car> GetAll()
        {
            return _cars.GetAll();
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            _cars.Update(car);
        }

        public void Delete(Car car)
        {
            _cars.Delete(car);
        }
    }
}
