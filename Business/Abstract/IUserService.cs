using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<List<User>> GetByCustomerId(int id);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}
