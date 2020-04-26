using APBD_mock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_mock.DTOs
{
    public class AnimalRequest
    {
        public int IdAnimal { get; set; }
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int IdOwner { get; set; }
        public List<Procedure> UnderwentProcedure { get; set; }
    }
}
