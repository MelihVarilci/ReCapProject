using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        // Arabaları saklayabilmesi için Liste tipinde _cars değişkenimizi oluşturduk.
        List<Car> _cars;

        public InMemoryCarDal()
        {
            // Constructor sayesinde araba listesini hazırladık.
            _cars = new List<Car>
            {
                new Car{CarId = 1, BrandId = 1, ColorId = 1, CarDailyPrice = 120, CarModelYear = "2018", CarDescription = "Öğretmenden"},
                new Car{CarId = 2, BrandId = 1, ColorId = 2, CarDailyPrice = 100, CarModelYear = "2016", CarDescription = "Öğrenciden"},
                new Car{CarId = 3, BrandId = 2, ColorId = 1, CarDailyPrice = 150, CarModelYear = "2019", CarDescription = "Doktordan"},
                new Car{CarId = 4, BrandId = 2, ColorId = 2, CarDailyPrice = 240, CarModelYear = "2021", CarDescription = "Mühendisten"},
                new Car{CarId = 5, BrandId = 2, ColorId = 3, CarDailyPrice = 200, CarModelYear = "2020", CarDescription = "Muhasebeciden"},

            };
        }
        
        //public List<Car> GetById(int carId)
        //{
        //    /* Constructor sayesinde elde ettiğimiz araba listesinden LINQ kullanarak
        //       göndermiş olduğumuz araba ID'sine sahip araba nesnesini geri döndürdük.*/
        //    return _cars.Where(c=>c.CarId==carId).ToList();
        //}
        //public List<Car> GetAll()
        //{
        //    // Constructor sayesinde elde ettiğimiz araba listesini geri döndürdük.
        //    return _cars;
        //}

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars;
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Car car)
        {
            // Listeye arabayı ekledik.
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            // LINQ kullanılarak foreach döngüsünü kullanmaktan kurtulduk.
            // Aynı zamanda göndermiş olduğumuz ürün id'sine sahip olan listedeki arabayı bulup 'carToUpdate' e atamasını sağladık. 
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            // 'carToUpdate' deki değişkenlerin güncellenmesini sağladık.
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.CarDailyPrice = car.CarDailyPrice;
            carToUpdate.CarDescription = car.CarDescription;
            carToUpdate.CarModelYear = car.CarModelYear;

        }

        public void Delete(Car car)
        {
            // LINQ kullanılarak foreach döngüsünü kullanmaktan kurtulduk.
            // Aynı zamanda göndermiş olduğumuz ürün id'sine sahip olan listedeki arabayı bulup 'carToDelete' e atamasını sağladık. 
            Car carToDelete = _cars.SingleOrDefault(c=> c.CarId == car.CarId);

            // Listeden arabayı kaldırdık.
            _cars.Remove(carToDelete);
        }
    }
}
