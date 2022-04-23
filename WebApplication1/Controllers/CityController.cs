using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {

       [HttpGet]
       public List<City> getCity()
       {
            return City.GetAllCity();
       }

        [HttpGet("{id}")]
        public City getCityById(int id)
        {
            return City.GetCityById(id);
        }
    }
}
