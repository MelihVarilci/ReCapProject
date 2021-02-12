﻿using System;
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
        
        public List<Car> GetByDailyPrice(decimal min, decimal max)
        {
            return _cars.GetAll(p => p.DailyPrice >= min && p.DailyPrice <= max);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _cars.GetAll(p => p.BrandId == id);
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _cars.GetAll(p => p.ColorId == id);
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
