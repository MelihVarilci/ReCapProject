using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            
            // Yeni bir araba nesnesi oluşturuyoruz ve ilk atamalarını gerçekleştiriyoruz.
            Car car1 = new Car()
            {
                BrandId = 2,
                ColorId = 2,
                CarDailyPrice = 90,
                CarDescription = "Savcıdan",
                CarModelYear = "2011"
            };

            // Tüm araba listesini yazdırıyoruz.
            foreach (var car in carManager.GetAll())
            {
                System.Console.WriteLine(car.CarId+" nolu araba "+car.CarModelYear+" model ve "+car.CarDescription);   
            }

            // Arabanın Description'ı 2 Harften Eşit veya Fazlayse ve DailyPrice'ı 0 dan Büyükse Database'e ekleme işlemi yapıyoruz.
            if (car1.CarDescription.Length >= 2 && car1.CarDailyPrice > 0)
            {
                // Araba listesine yen bir araba ekliyoruz.
                carManager.Add(car1);
            }

            // Eklediğimiz arabayı listeden kaldırıyoruz.
            //carManager.Delete(car1);

            System.Console.WriteLine("**********************\nEklenen Arabanın Özellikleri ;");
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            // Databese'e Araba Eklerken İlk Değer Olarak CarId Atayamadığımızdan Dolayı Manuel Olarak Kendimiz Atıyoruz.
            car1.CarId = 8;

            // Tüm Araba Listesinden CarId'si 8 Olan Arabanın Verilerini Yazdırıyoruz.
            foreach (var car in carManager.GetAll())
            {
                if (car.CarId == car1.CarId)
                {
                    System.Console.WriteLine(car.CarId + " nolu araba " + car.CarModelYear + " model ve " + car.CarDescription + " kiralık.");
                }
            }

            // Aynı Araba İçin Marka Bilgisini Getiriyoruz.
            foreach (var car in brandManager.GetByBrandId(car1.BrandId))
            {
                System.Console.WriteLine("Markası : " + car.BrandName);
            }

            ColorManager colorManager = new ColorManager(new EfColorDal());


            // Aynı Araba İçin Renk Bilgisini Getiriyoruz.
            foreach (var car in colorManager.GetByColorId(car1.ColorId))
            {
                System.Console.WriteLine("Rengi : " + car.ColorName);
            }

            System.Console.WriteLine("**********************");
            // Tüm Bu İşlemlerden Sonra Araba Listesini Tekrardan Yazdırıyoruz.
            foreach (var car in carManager.GetAll())
            {
                System.Console.WriteLine(car.CarId + " nolu araba " + car.CarModelYear + " model ve " + car.CarDescription);
            }


        }
    }
}
