using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD_mock.DTOs;
using APBD_mock.Models;
using APBD_mock.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_mock.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private IAnimalsDbService _service;
        public AnimalsController(IAnimalsDbService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getAnimals")]
        // Np: GetAnimals("name", "asc");
        public IActionResult GetAnimals(string sortBy, string order)
        {
                return StatusCode(200, _service.GetAnimals(sortBy, order));
        }

        [HttpPost]
        [Route("addAnimal")]
        public IActionResult AddAnimal([FromBody] AnimalRequest animal)
        {
            return StatusCode(200, _service.InsertAnimal(animal));
        }


    }
}