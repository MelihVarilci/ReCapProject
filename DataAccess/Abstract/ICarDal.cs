using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        // Kullanmak istediğimiz methodları oluşturuyoruz.
        List<CarDetailDto> GetCarDetails();

    }
}
