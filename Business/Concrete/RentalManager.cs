using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICustomerService customerService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByRentalId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentalId == id));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCarId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == id));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCustomerId(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == id));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByRentDate(DateTime first, DateTime last)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentDate == first && r.ReturnDate == last));
        }

        [CacheAspect]
        public IDataResult<Rental> CheckReturnDate(int carId)
        {
            List<Rental> resultRental = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null);
            if (resultRental.Count > 0)
            {
                return new ErrorDataResult<Rental>(Messages.RentalUndeliveredCar);
            }

            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.CarId == carId));
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(r => r.CarId == carId), Messages.RentalListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(r => r.CustomerId == customerId), Messages.RentalListed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CarRentedCheck(rental),
                FindeksScoreCheck(rental.CustomerId, rental.CarId),
                UpdateCustomerFindexPoint(rental.CustomerId, rental.CarId));

            if (result != null)
                return result;

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }


        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }


        [SecuredOperation("car.delete")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        private IResult CarRentedCheck(Rental rental)
        {
            var rentalledCars = _rentalDal.GetAll(r => r.CarId == rental.CarId && (r.ReturnDate == null || r.ReturnDate > DateTime.Now)).Any();

            if (rentalledCars)
                return new ErrorResult(Messages.CarIsStillRentalled);

            return new SuccessResult();
        }

        private IResult FindeksScoreCheck(int customerId, int carId)
        {
            var customerFindexPoint = _customerService.GetByCustomerId(customerId).Data[0].FindexPoint;

            if (customerFindexPoint == 0)
                return new ErrorResult(Messages.CustomerFindexPointIsZero);

            var carFindexPoint = _carService.GetById(carId).Data.FindexPoint;

            if (customerFindexPoint < carFindexPoint)
                return new ErrorResult(Messages.CustomerScoreIsInsufficient);

            return new SuccessResult();
        }

        private IResult UpdateCustomerFindexPoint(int customerId, int carId)
        {
            var customer = _customerService.GetByCustomerId(customerId).Data[0];
            var car = _carService.GetById(carId).Data;

            customer.FindexPoint = (car.FindexPoint / 2) + customer.FindexPoint;

            _customerService.Update(customer);
            return new SuccessResult();
        }
    }
}
