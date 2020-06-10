using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_GC_Car_Dealership.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Capstone_GC_Car_Dealership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly CarDealershipDbContext _context;

        public CarTypesController(CarDealershipDbContext context)
        {
            _context = context;
        }

        //GET: api/CarTypes/{id}
        [HttpGet("{id}")]
        public async Task<CarTypes> GetCarById(int id)
        {
            CarTypes car = await _context.CarTypes.FindAsync(id);

            return car;
        }
        //GET: api/CarTypes?make={make}&model={model}&year={year}&color={color} - make & model
        [HttpGet]
        public async Task<ActionResult<List<CarTypes>>> GetAllCarTypesByMake(string make, string model, string year, string color)
        {
            var carTypes = await _context.CarTypes.ToListAsync();
            if (make != null)
            {
                make = make.ToLower();
                var filtered = carTypes.Where(x => x.Make.ToLower() == make && x.Model.ToLower() == model.ToLower()).ToList();

                return filtered;
            }
            return carTypes;
        }

        //GET: 
        [HttpGet]
        public async Task<ActionResult<List<CarTypes>>> SearchForCars(string? make, string? model, int? year, string? color)
        {
            return Ok(await _context.CarTypes.Where(x => x.Make.Contains(make) || x.Model.Contains(model) || x.Color.Contains(color) || x.Year == year).ToListAsync());
        }

        //[HttpGet("{make}&model={model}&year={year}&color={color}")]
        //public string Get(string make = null, string model = null, string year = null, string color = null)
        //{
        //    var result = _context.CarTypes.Where(c => (c.Make == make || c.Make == null) || (c.Model == model || c.Model == null) || (c.Color == color || c.Color == null) || (c.Year == year || c.Year == null)).ToList();
        //    var something = JsonConvert.SerializeObject(result);
        //    return something;
        //}


        //GET: api/CarTypes?Search={keyword} - make & model and year
        //[HttpGet]
        //public async Task<ActionResult<List<CarTypes>>> GetCarTypesByMakeModel(string make, string model)
        //{
        //    var carTypes = await _context.CarTypes.ToListAsync();
        //    if (make != null && model != null)
        //    {
        //        make = make.ToLower();
        //        model = model.ToLower();

        //        var filtered = carTypes.Where(x => x.Make.ToLower() == make &&
        //                                       x.Model.ToLower() == model).ToList();

        //        return filtered;
        //    }
        //    return carTypes;
        //}

        [HttpGet("search")]
        public async Task<ActionResult<List<CarTypes>>> SearchForCars(string? make, string? model, string? year, string? color)
        {
            return Ok(await _context.CarTypes.Where(x => x.Make.Contains(make) || x.Model.Contains(model) || x.Color.Contains(color) || x.Year.Contains(year.ToString()).ToListAsync()));
        }
    }
}
