﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_mock.DTOs
{
    public class AnimalsResponse
    {
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public string LastNameOfOwner { get; set; }
    }
}
