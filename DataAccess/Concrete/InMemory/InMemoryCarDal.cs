using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Abstract;
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
                new Car{CarId = 1, BrandId = 1, ColorId = 1, DailyPrice = 120, ModelYear = "2018", Description = "Öğretmenden"},
                new Car{CarId = 2, BrandId = 1, ColorId = 2, DailyPrice = 120, ModelYear = "2016", Description = "Öğrenciden"},
                new Car{CarId = 3, BrandId = 2, ColorId = 1, DailyPrice = 120, ModelYear = "2019", Description = "Doktordan"},
                new Car{CarId = 4, BrandId = 2, ColorId = 2, DailyPrice = 120, ModelYear = "2021", Description = "Mühendisten"},
                new Car{CarId = 5, BrandId = 2, ColorId = 3, DailyPrice = 120, ModelYear = "2020", Description = "Muhasebeciden"},

            };
        }

        public List<Car> GetById(int carId)
        {
            /* Constructor sayesinde elde ettiğimiz araba listesinden LINQ kullanarak
               göndermiş olduğumuz araba ID'sine sahip araba nesnesini geri döndürdük.*/
            return _cars.Where(c=>c.CarId==carId).ToList();
        }

        public List<Car> GetAll()
        {
            // Constructor sayesinde elde ettiğimiz araba listesini geri döndürdük.
            return _cars;
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
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;

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
