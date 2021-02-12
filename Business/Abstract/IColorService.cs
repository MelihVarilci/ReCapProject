using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        List<Color> GetAll();
        List<Color> GetByColorId(int id);
        void Add(Color brand);
        void Update(Color brand);
        void Delete(Color brand);
    }
}
