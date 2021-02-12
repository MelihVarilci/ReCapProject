using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IColorDal:IEntityRepository<Color>
    {
    }
}
