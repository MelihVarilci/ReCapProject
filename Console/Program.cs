using System;
using Business.Concrete;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kiralanacak araba bilgileri giriliyor ve bu bilgilere göre arabanın kiralanabilmesinin
            // uygunluğu kontrol edilerek araba kiralamaya çalışılıyor ve Kiralanan arabaların listesini 
            // ayrıntılı bilgileriyle getiriyor.
            RentalManager();

            // Car, Brand ve Color tablolarının join edilerek istenilen ortak değerlerin elde edilmesi.
            //CarManager1();

            // Id ile ilgili markanın id'sinin database'teki marka değerini öğrenme.
            //BrandManager();

            // Id ile ilgili renk id'sinin database'teki renk değerini öğrenme.
            //ColorManager();

            // Uzun amaçlı bir kod açıklamasını yap.
            //CarManager(car1);
        }

        private static void RentalManager()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            Rental rental = new Rental()
            {
                CarId = 6,
                CustomerId = 5,
                RentDate = DateTime.Now,
                ReturnDate = null
            };
            var result = rentalManager.Add(rental);
            if (!result.Success) Console.WriteLine(result.Message);
            rentalManager.GetAll();
            rentalManager.GetAll().Data.ForEach(r => Console.WriteLine(r.CarId + " " + r.RentDate));

            var resultRentalMenager = rentalManager.GetRentalDetails();
            foreach (var value in resultRentalMenager.Data)
            {
                Console.WriteLine("CarId: " + value.CarId + "\tFirstname: " + value.FirstName + "\tLastName: " +
                                  value.LastName + "\tCompany Name: " + value.CompanyName + "\tBrand: "
                                  + value.BrandName + "\tColor: " + value.ColorName + "\tRentDate: " + value.RentDate +
                                  "\tReturnDate: " + value.ReturnDate);
            }
        }

        private static void CarManager1()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    System.Console.WriteLine(
                        "Car Name : " + car.CarDescription +
                        "\nBrand Name : " + car.BrandName +
                        "\nColor Name : " + car.ColorName +
                        "\nDail Price : " + car.CarDailyPrice +
                        "\n*******************"
                    );
                }
            }
        }

        private static void BrandManager()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            var result = brandManager.GetByBrandId(2);
            if (result.Success == true)
            {
                foreach (var brand in result.Data)
                {
                    System.Console.WriteLine(brand.BrandId + " nolu marka : " + brand.BrandName);
                }
            }
        }

        private static void ColorManager()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            var result = colorManager.GetByColorId(2);
            if (result.Success == true)
            {
                foreach (var color in result.Data)
                {
                    System.Console.WriteLine(color.ColorId + " nolu renk : " + color.ColorName);
                }
            }
        }

        private static void CarManager(Car car1)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            // Tüm araba listesini yazdırıyoruz.
            foreach (var car in carManager.GetAll().Data)
            {
                System.Console.WriteLine(car.CarId + " nolu araba " + car.CarModelYear + " model ve " + car.CarDescription);
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
            foreach (var car in carManager.GetAll().Data)
            {
                if (car.CarId == car1.CarId)
                {
                    System.Console.WriteLine(car.CarId + " nolu araba " + car.CarModelYear + " model ve " + car.CarDescription +
                                             " kiralık.");
                }
            }

            // Aynı Araba İçin Marka Bilgisini Getiriyoruz.
            var resultBrand = brandManager.GetByBrandId(car1.BrandId);
            if (resultBrand.Success == true)
            {
                foreach (var car in resultBrand.Data)
                {
                    System.Console.WriteLine("Markası : " + car.BrandName);
                }
            }

            ColorManager colorManager = new ColorManager(new EfColorDal());


            // Aynı Araba İçin Renk Bilgisini Getiriyoruz.
            var resultColor = colorManager.GetByColorId(car1.ColorId);
            if (resultColor.Success == true)
            {
                foreach (var car in resultColor.Data)
                {
                    System.Console.WriteLine("Rengi : " + car.ColorName);
                }
            }

            System.Console.WriteLine("**********************");
            // Tüm Bu İşlemlerden Sonra Araba Listesini Tekrardan Yazdırıyoruz.
            foreach (var car in carManager.GetAll().Data)
            {
                System.Console.WriteLine(car.CarId + " nolu araba " + car.CarModelYear + " model ve " + car.CarDescription);
            }
        }
    }
}