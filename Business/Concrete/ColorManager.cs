using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager:IColorService
    {
        IColorDal _color;

        public ColorManager(IColorDal color)
        {
            _color = color;
        }

        public List<Color> GetAll()
        {
            return _color.GetAll();
        }

        public List<Color> GetByColorId(int id)
        {
            return _color.GetAll(p => p.ColorId == id);
        }

        public void Add(Color brand)
        {
            _color.Add(brand);
        }

        public void Update(Color brand)
        {
            _color.Update(brand);
        }

        public void Delete(Color brand)
        {
            _color.Delete(brand);
        }
    }
}
