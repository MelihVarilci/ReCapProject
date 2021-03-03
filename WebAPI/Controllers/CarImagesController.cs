using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.FileHelper;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm(Name = ("Id"))] int id)
        {
            var result = _carImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarimagesbycarid")]
        public IActionResult GetCarImagesByCarId([FromForm(Name = ("CarId"))] int carId)
        {
            var result = _carImageService.GetCarImagesByCarId(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult AddAsync([FromForm(Name = ("Image"))] IFormFile file ,[FromForm] CarImage carImage)
        {
            carImage.ImageDate = DateTime.Now;
            carImage.ImagePath = FileHelper.AddAsync(file);
            var result = _carImageService.Add(carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var oldPath = Path.Combine(Environment.CurrentDirectory, "wwwroot",
                _carImageService.Get(carImage.Id).Data.ImagePath);

            carImage.ImagePath = FileHelper.UpdateAsync(oldPath, file);

            var result = _carImageService.Update(carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int id)
        {
            var carImage = _carImageService.Get(id).Data;

            //Aynı işlevdeki başka bir kod yazımı;
            //var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
            //              _carImageService.Get(carImage.Id).Data.ImagePath;
            
            var oldPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", _carImageService.Get(id).Data.ImagePath);

            FileHelper.DeleteAsync(oldPath);

            var result = _carImageService.Delete(carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
