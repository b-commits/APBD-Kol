using APBD_mock.DTOs;
using APBD_mock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_mock.Services
{
    public interface IAnimalsDbService
    {
        public List<AnimalsResponse> GetAnimals(string sortBy, string ascDesc);
        public Animal InsertAnimal(AnimalRequest animal);
    }
}
