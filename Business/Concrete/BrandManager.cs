using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager:IBrandService
    {
        IBrandDal _brand;

        public BrandManager(IBrandDal brand)
        {
            _brand = brand;
        }

        public List<Brand> GetAll()
        {
            return _brand.GetAll();
        }

        public List<Brand> GetByBrandId(int id)
        {
            return _brand.GetAll(p => p.BrandId == id);
        }

        public void Add(Brand brand)
        {
            _brand.Add(brand);
        }

        public void Update(Brand brand)
        {
            _brand.Update(brand);
        }

        public void Delete(Brand brand)
        {
            _brand.Delete(brand);
        }
    }
}
