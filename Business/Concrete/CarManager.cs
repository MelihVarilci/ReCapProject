using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _cars;

        public CarManager(ICarDal cars)
        {
            _cars = cars;
        }


        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_cars.GetAll(c => c.CarDailyPrice >= min && c.CarDailyPrice <= max),Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_cars.GetAll(p => p.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_cars.GetAll(p => p.ColorId == id));
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarDescriptionInvalid);
            }
            return new SuccessDataResult<List<Car>>(_cars.GetAll(),Messages.CarListed);
        }

        public IResult Add(Car car)
        {
            if (car.CarDescription.Length<2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _cars.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Update(Car car)
        {
            if (car.CarDescription.Length < 2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _cars.Update(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            if (car.CarDescription.Length < 2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _cars.Delete(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_cars.GetCarDetails(),Messages.CarListed);
        }
    }
}