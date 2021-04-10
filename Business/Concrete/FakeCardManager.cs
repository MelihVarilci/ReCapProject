using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class FakeCardManager : IFakeCardService
    {
        private IFakeCardDal _fakeCardDal;

        public FakeCardManager(IFakeCardDal fakeCardDal)
        {
            _fakeCardDal = fakeCardDal;
        }

        public IDataResult<List<FakeCard>> GetByCardNumber(string cardNumber)
        {
            return new SuccessDataResult<List<FakeCard>>(_fakeCardDal.GetAll(c => c.CardNumber == cardNumber));
        }

        public IDataResult<List<FakeCard>> GetAll()
        {
            return new SuccessDataResult<List<FakeCard>>(_fakeCardDal.GetAll());
        }

        public IDataResult<List<FakeCard>> GetCardsByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<FakeCard>>(_fakeCardDal.GetAll(c => c.CustomerId == customerId));
        }

        public IDataResult<FakeCard> GetById(int carId)
        {
            return new SuccessDataResult<FakeCard>(_fakeCardDal.Get(c => c.Id == carId));
        }

        public IResult IsCardExist(FakeCard fakeCard)
        {
            var result = _fakeCardDal.Get(c => c.NameOnTheCard == fakeCard.NameOnTheCard && c.CardNumber == fakeCard.CardNumber);
            if (result == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IResult Add(FakeCard fakeCard)
        {

            var card = _fakeCardDal.Get(f => f.CardNumber == fakeCard.CardNumber && f.CustomerId == fakeCard.CustomerId);
            if (card != null)
            {
                return new SuccessResult();
            }

            _fakeCardDal.Add(fakeCard);
            return new SuccessResult();
        }

        public IResult Delete(FakeCard fakeCard)
        {
            _fakeCardDal.Delete(fakeCard);
            return new SuccessResult();
        }

        public IResult Update(FakeCard fakeCard)
        {
            _fakeCardDal.Update(fakeCard);
            return new SuccessResult();
        }
    }
}
