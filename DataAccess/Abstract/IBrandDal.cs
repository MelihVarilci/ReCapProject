using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public interface IBrandDal : IEntityRepository<Brand>
    {
    }
}