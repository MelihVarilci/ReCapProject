using System;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());
            
            // Yeni bir araba nesnesi oluşturuyoruz ve ilk atamalarını gerçekleştiriyoruz.
            Car car1 = new Car()
            {
                BrandId = 1,
                CarId = 6,
                ColorId = 3,
                DailyPrice = 175,
                Description = "Veterinerden",
                ModelYear = "2121"
            };

            // Tüm araba listesini yazdırıyoruz.
            foreach (var car in carManager.GetAll())
            {
                System.Console.WriteLine(car.CarId+" nolu araba "+car.ModelYear+" model ve "+car.Description);   
            }

            // Araba listesine yen bir araba ekliyoruz.
            carManager.Add(car1);
            System.Console.WriteLine("**********************");

            // Eklenen arabayı ekrana yazdırıyoruz.
            foreach (var car in carManager.GetById(car1.CarId))
            {
                System.Console.WriteLine(car.CarId + " nolu araba: " + car.Description + " satılık.");
            }
            System.Console.WriteLine("**********************");

            // Eklediğimiz arabayı listeden kaldırıyoruz.
            carManager.Delete(car1);

            // Tüm araba listesini yazdırıyoruz.
            foreach (var car in carManager.GetAll())
            {
                System.Console.WriteLine(car.CarId + " nolu araba " + car.ModelYear + " model ve " + car.Description);
            }

        }
    }
}
