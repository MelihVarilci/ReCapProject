using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _color;

        public ColorManager(IColorDal color)
        {
            _color = color;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_color.GetAll(),Messages.ColorListed);
        }

        public IDataResult<List<Color>> GetByColorId(int id)
        {
            return new SuccessDataResult<List<Color>>(_color.GetAll(p => p.ColorId == id),Messages.ColorListed);
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color brand)
        {
            _color.Add(brand);
            return new SuccessResult(Messages.ColorAdded);
        }
        
        public IResult Update(Color brand)
        {
            _color.Update(brand);
            return new SuccessResult(Messages.ColorUpdated);
        }

        public IResult Delete(Color brand)
        {
            _color.Delete(brand);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}