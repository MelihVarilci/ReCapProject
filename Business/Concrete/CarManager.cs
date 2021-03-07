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
using System.Threading;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

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

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_cars.GetAll(p => p.BrandId == id));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_cars.GetAll(p => p.ColorId == id));
        }

        [CacheAspect(duration: 10)]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(5000);
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarDescriptionInvalid);
            }
            return new SuccessDataResult<List<Car>>(_cars.GetAll(),Messages.CarListed);
        }

        [SecuredOperation("car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            // business codes
            _cars.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("car.update")]
        [CacheRemoveAspect("ICarService.Get")]
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

        [SecuredOperation("car.delete")]
        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            if (car.CarDescription.Length < 2)
            {
                return new ErrorResult(Messages.CarDescriptionInvalid);
            }
            _cars.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_cars.GetCarDetails(),Messages.CarListed);
        }
        
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.CarDailyPrice<100)
            {
                throw new Exception("");
            }

            Add(car);
            return null;
        }
    }
}