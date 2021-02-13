using System;
using System.Collections.Generic;
using System.Text;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarModelYear { get; set; }
        public decimal CarDailyPrice { get; set; }
        public string CarDescription { get; set; }
    }
}
