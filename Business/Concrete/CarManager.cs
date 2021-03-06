using Business.Abstract;
using Business.Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

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

        [SecuredOperation("car.add")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            // business codes
            _cars.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("car.Update")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            if (car.CarDescription.Length < 2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _cars.Update(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("car.Delete")]
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